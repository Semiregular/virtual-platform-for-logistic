using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using Algo.Random;
using Config;
using Entity.Lifts;
using Entity.Objs;
using Manager;
using UI.Metrics;
using UnityEngine;
using UnityEngine.Networking;
using Util;
using Stack = Entity.Stacks.Stack;


namespace Entity.Tasks
{
    public class BaseTaskAllocator : MonoBehaviour
    {
        protected static readonly TaskConfig TaskConfig = GlobalConfig.taskConfig;
        
        protected static volatile ConcurrentQueue<Task> taskQueue = new ();
        protected static readonly object TaskQueueLock = new ();
        
        protected static volatile ConcurrentDictionary<int,BaseLift> liftDict = new();
        protected static readonly object LiftListLock = new ();
        
        protected bool IsAllowSend = true;
        protected bool IsAllowRev = true;
        protected bool IsAllowInc = true;
        protected bool IsAllowAssign = true;
        
        
        /// <summary>
        /// 开始执行任务协程
        /// </summary>
        public virtual void Run()
        {
            IsAllowRev = true;
            IsAllowAssign = true;
            IsAllowSend = true;
            IsAllowInc = true;
            //InitMap();
            // 启动任务自动生成
            if (GlobalConfig.isTaskAutoInc) StartCoroutine(TaskAutoInc());
            
            // 启动任务分配
            var s = GlobalConfig.isTaskAutoAssign ? 
                StartCoroutine(TaskAutoAssign()) : 
                StartCoroutine(InitTaskAssign());
            
            // 启动位置发送和命令接收
            if (GlobalConfig.enableRcsControl) StartCoroutine(InitWebSocket());
        }
    
        /// <summary>
        /// 任务自动生成 协程
        /// </summary>
        protected virtual IEnumerator TaskAutoInc()
        {
            while (GlobalConfig.isTaskAutoInc && IsAllowInc)
            {
                if (Task.taskCount < TaskConfig.TaskNum)
                {
                    Task task = new();
                    taskQueue.Enqueue(task);
                }
                yield return new WaitForSeconds(TaskConfig.TaskIncSecond);
            }
            yield return null;
        }
    
        /// <summary>
        /// 任务自动分配 协程 
        /// </summary>
        /// <remarks>算法需要重写</remarks>
        /// <seealso cref="RandomTaskAllocator"/>
        protected virtual IEnumerator TaskAutoAssign() {
            yield return null;
        }

        /// <summary>
        /// 任务手动分配 协程
        /// </summary>
        private async System.Threading.Tasks.Task AssignReceiveWebSocket()
        {
            var revWebSocket = new ClientWebSocket();
            await revWebSocket.ConnectAsync(
                new Uri(GlobalConfig.assignRevWebSocketUrl), 
                CancellationToken.None);
            
            var segment = new byte[4096];
            try
            {
                while (IsAllowRev && revWebSocket.State == WebSocketState.Open)
                {
                    var buffer = new ArraySegment<byte>(segment);
                    var result = await revWebSocket.ReceiveAsync(buffer, CancellationToken.None);
                    if (result.Count == 0) continue;
                    if (buffer.Array == null) continue;
                    var json = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                    var commandList = NewJsonUtil.FromJson<List<TaskCommand>>(json);

                    foreach (var command in commandList)
                    {
                        AddLift(command);
                    }
                }
            }catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
            revWebSocket.Dispose();
        }

        protected virtual void InitMap()
        {
            var map = NewJsonUtil.ToJson(GlobalConfig.mapConfig);
            StartCoroutine(SendHttpPost(map));
        }
        
        /// <summary>
        /// Http Post
        /// </summary>
        protected virtual IEnumerator SendHttpPost(string json)
        {
            var data = Encoding.UTF8.GetBytes(json);
            var request = new UnityWebRequest(GlobalConfig.mapHttpUrl, "POST");
            request.uploadHandler = new UploadHandlerRaw(data);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || 
                request.result == UnityWebRequest.Result.ProtocolError)
                Debug.LogError(request.error);
            
            yield return null;
        }

        /// <summary>
        /// 添加机器人
        /// </summary>
        /// <param name="command"></param>
        /// <exception cref="Exception"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected virtual void AddLift(TaskCommand command)
        {
            switch (command.Type)
            {
                case  LiftType.Kiva:
                    var stack = StackManager.Instance.StackList
                        .FirstOrDefault(x => x.StackId == command.StackId);
                    if (stack == null)
                    {
                        throw  new Exception("stack not found");
                    }
                    AddKivaLift(stack);
                    break;
                case  LiftType.Ctu:
                    List<Box> boxNotReady = new();
                    foreach (var boxId in command.BoxNotReady)
                    {
                        var box = ObjManager.Instance.BoxList
                            .FirstOrDefault(x => x.BoxId == boxId);
                        if (box == null)
                        {
                            throw  new Exception("box not found");
                        }
                        boxNotReady.Add(box);
                    }
                    AddCtuLift(boxNotReady);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 添加 Kiva 机器人
        /// </summary>
        /// <param name="stack"></param>
        /// <exception cref="Exception"></exception>
        protected virtual void AddKivaLift(Stack stack)
        {
            var kivaLift = new KivaLift(stack);
            
            if(!liftDict.TryAdd(kivaLift.LiftId, kivaLift)) 
                throw  new Exception("lift id conflict");
            
            ObjManager.Instance.RemoveStack(stack);
        }

        /// <summary>
        /// 添加 CTU 机器人
        /// </summary>
        /// <param name="boxNotReady"></param>
        /// <exception cref="Exception"></exception>
        protected virtual void AddCtuLift(List<Box> boxNotReady)
        {
            var ctuLift = new CtuLift(boxNotReady);
            
            if(!liftDict.TryAdd(ctuLift.LiftId, ctuLift))
                throw  new Exception("lift id conflict");
            
            foreach (var box in boxNotReady)
            {
                ObjManager.Instance.RemoveObjMap(box);
            }
            ObjManager.Instance.BoxList.RemoveAll(box => boxNotReady.Contains(box));
            Statistics.Instance.boxAssign += boxNotReady.Count;
        }

        /// <summary>
        /// 标记机器人已移除
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        public virtual void RemoveLift(int id)
        {
            if (liftDict.TryGetValue(id, out var lift))
            {
                lift.IsRemoved = true;
                liftDict[id]= lift;
            }
            else
            {
                throw new Exception("lift remove failed");
            }
        }

        /// <summary>
        /// 从列表中移除机器人
        /// </summary>
        /// <param name="idList"></param>
        protected virtual void DestroyLift(List<int> idList)
        {
            foreach (var id in idList)
            {
                liftDict.TryRemove(id, out _);
            }
        }
        
        /// <summary>
        /// 机器人位置信息 发送
        /// </summary>
        private async System.Threading.Tasks.Task PositionSendWebSocket()
        {
            var sendWebSocket = new ClientWebSocket();
            await sendWebSocket.ConnectAsync(
                new Uri(GlobalConfig.positionSendWebSocketUrl), 
                CancellationToken.None);
            await System.Threading.Tasks.Task.Delay(1000);
            try
            {
                while (IsAllowSend && sendWebSocket.State == WebSocketState.Open)
                {
                    List<BaseLift> valueList = new();
                    List<int> removeList = new();
                    
                    if (liftDict.Count == 0) continue;
                    foreach (var lift in liftDict)
                    {
                        // 机器人已移除
                        if (lift.Value.IsRemoved)
                        { 
                            // 收集待移除的机器人
                            removeList.Add(lift.Key);
                            continue;
                        }
                        // 更新位置
                        lift.Value.UpdatePosition();
                        valueList.Add(lift.Value);
                    } 
                    
                    var json = NewJsonUtil.ToJson(valueList);
                    var data = Encoding.UTF8.GetBytes(json);
                    await sendWebSocket.SendAsync(
                        new ArraySegment<byte>(data), 
                        WebSocketMessageType.Text, true, CancellationToken.None);
                    // 移除机器人
                    DestroyLift(removeList);
                    await System.Threading.Tasks.Task.Delay(GlobalConfig.positionSendInterval); 
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
            
            sendWebSocket.Dispose();
        }
        
        /// <summary>
        /// 接收控制命令
        /// </summary>
        private async System.Threading.Tasks.Task CommandReceiveWebSocket()
        {
            var revWebSocket = new ClientWebSocket();
            await revWebSocket.ConnectAsync(
                new Uri(GlobalConfig.commandRevWebSocketUrl), 
                CancellationToken.None);
            
            var segment = new byte[4096];
            try
            {
                while (IsAllowRev && revWebSocket.State == WebSocketState.Open)
                {
                    var buffer = new ArraySegment<byte>(segment);
                    var result = await revWebSocket.ReceiveAsync(buffer, CancellationToken.None);
                    if (result.Count == 0) continue;
                    if (buffer.Array == null) continue;
                    var json = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                    var commandList = NewJsonUtil.FromJson<List<LiftCommand>>(json);

                    // 执行命令
                    foreach (var command in commandList)
                    {
                        var id = command.Id;
                        // 找到对应机器人
                        if (liftDict.TryGetValue(id, out var lift))
                        {
                            var liftController = lift.LiftController;
                            // 执行命令
                            liftController.ExecCommand(command);
                        }
                    }
                }
            }catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
            revWebSocket.Dispose();
        }
        
        /// <summary>
        /// 初始化 WebSocket
        /// </summary>
        /// <returns></returns>
        private IEnumerator InitWebSocket()
        {
            
            var positionSendTask = PositionSendWebSocket();
            var commandReceiveTask = CommandReceiveWebSocket();
            while (!positionSendTask .IsCompleted || 
                   !commandReceiveTask.IsCompleted)
            {
                yield return null;
            }
        }

        private IEnumerator InitTaskAssign()
        {
            var assignTask = AssignReceiveWebSocket();
            while (!assignTask.IsCompleted)
            {
                yield return null;
            }
        }

        /// <summary>
        /// 停止 WebSocket，清空数据
        /// </summary>
        public void Stop()
        {
            IsAllowSend = false;
            IsAllowRev = false;
            IsAllowAssign = false;
            IsAllowInc = false;
            taskQueue.Clear();
            liftDict.Clear();
        }

        protected virtual void OnDestroy()
        {
            Stop();
        }
        
    }
}
