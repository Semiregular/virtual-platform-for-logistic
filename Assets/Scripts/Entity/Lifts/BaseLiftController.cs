using System;
using System.Collections;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using DG.Tweening;
using Config;
using Entity.Tasks;
using UI.Metrics;
using UnityEngine;
using UnityEngine.Networking;
using Task = System.Threading.Tasks.Task;

namespace Entity.Lifts
{
    public class BaseLiftController : MonoBehaviour
    {
        private const float ResumeTime = 2f;
        private const float RaycastDistance = 1.5f;
        
        public int liftId;
        private Vector3 rayPosition;
        protected float RayHeight = 1f;
        
        private new Rigidbody rigidbody;
        private bool isAllowSend = true;
        private bool isAllowRev = true;
        
        public bool isAnimationPaused;
        public bool isPickUp;
        
        protected Sequence AnimationSeq;
        
   
        protected void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            InitRayHeight();
            InitProperties();
            
            // 未启用 RCS 中央通讯，则启用单点收发
            if (!GlobalConfig.enableRcsControl)
            {
                StartCoroutine(InitWebSocket());
            }
        }
        
        /// <summary>
        /// 初始化变量
        /// </summary>
        protected virtual void InitProperties()
        {
           
        }

        /// <summary>
        /// 调整射线高度
        /// </summary>
        protected virtual void InitRayHeight()
        {
            
        }
        
    
        private void Update()
        {
            UpdateRayPosition();
        }

        /// <summary>
        /// 更新射线位置
        /// </summary>
        protected virtual void UpdateRayPosition()
        {
            rayPosition.x = transform.position.x;
            rayPosition.y = RayHeight;
            rayPosition.z = transform.position.z;
        }
        
        /// <summary>
        /// 绘制射线
        /// </summary>
        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(rayPosition, transform.forward * RaycastDistance);
        }
        
        /// <summary>
        /// 序列化运动状态
        /// </summary>
        private byte[] SerializeState()
        {
            var velocity = rigidbody.velocity;
            var direction = transform.forward;
            var position = transform.position;
            var state = new LiftState(liftId, position, direction, velocity);
            var json = JsonUtility.ToJson(state);
            return Encoding.UTF8.GetBytes(json);
        }
        
        /// <summary>
        /// 发送 http 请求 Post 方法
        /// </summary>
        protected virtual IEnumerator SendHttpPost()
        {

            var data = SerializeState();
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
        /// 异步发送 WebSocket 请求，发送序列化位置信息
        /// </summary>
        private async Task SendWebSocketAsync()
        {
            var sendWebSocket = new ClientWebSocket();
            await sendWebSocket.ConnectAsync(
                new Uri(GlobalConfig.positionSendWebSocketUrl+liftId), 
                CancellationToken.None);
            while (isAllowSend && sendWebSocket.State == WebSocketState.Open)
            {
                var data = SerializeState();
                await sendWebSocket.SendAsync(
                    new ArraySegment<byte>(data), 
                    WebSocketMessageType.Text, true, CancellationToken.None);
                // 延迟发送
                await Task.Delay(GlobalConfig.positionSendInterval); 
            }
            sendWebSocket.Dispose();
        }

        /// <summary>
        /// 异步接受 WebSocket 请求，持续接受运动控制命令
        /// </summary>
        private async Task ReceiveWebSocketAsync()
        {
            var revWebSocket = new ClientWebSocket();
            await revWebSocket.ConnectAsync(
                new Uri(GlobalConfig.commandRevWebSocketUrl+liftId), 
                CancellationToken.None);
            var segment = new byte[1024];
            while (isAllowRev && revWebSocket.State == WebSocketState.Open)
            {
                var buffer = new ArraySegment<byte>(segment);
                await revWebSocket.ReceiveAsync(buffer, CancellationToken.None);
                if (buffer.Array == null) continue;
                var json = Encoding.UTF8.GetString(buffer.Array);
                var command = JsonUtility.FromJson<LiftCommand>(json);
                // 执行命令
                ExecCommand(command);
                await Task.Delay(GlobalConfig.commandRevInterval);
            }
            revWebSocket.Dispose();
        }

        /// <summary>
        /// 初始化收发 WebSocket
        /// </summary>
        private IEnumerator InitWebSocket()
        {
            var sendTask = SendWebSocketAsync();
            var receiveTask = ReceiveWebSocketAsync();

            while (!sendTask.IsCompleted || !receiveTask.IsCompleted)
            {
                yield return null;
            }
        }

        /// <summary>
        /// 执行运动控制命令
        /// </summary>
        public virtual void ExecCommand(LiftCommand command)
        {
            var pos = transform.position;
            // nothing
            if(command.Type == LiftCommandType.Nothing) return;
            // 停止动画
            if (command.Type == LiftCommandType.Stop)
            {
                AnimationSeq.Kill();
                return;
            }
            // 取货
            if (command.Type == LiftCommandType.PickUp)
            {
                PickUp();
                return;
            }
            // 销毁
            if (command.Type == LiftCommandType.Destroy)
            {
                Destroy();
                return;
            }
            // 动画序列
            AnimationSeq = DOTween.Sequence();
            switch (command.Type)
            {
                case LiftCommandType.RelativeX:
                    AnimationSeq.Append(
                        transform.DOMoveX(
                            pos.x + command.Distance,
                            command.Time));
                    break;
                case LiftCommandType.RelativeY:
                    AnimationSeq.Append(
                        transform.DOMoveY(
                            pos.y + command.Distance,
                            command.Time));
                    break;
                case LiftCommandType.RelativeZ:
                    AnimationSeq.Append(
                        transform.DOMoveZ(
                            pos.z + command.Distance,
                            command.Time));
                    break;
                case LiftCommandType.Turn:
                    AnimationSeq.Append(
                        transform.DORotate(
                            new Vector3(0, command.Distance, 0), 
                            command.Time));
                    break;
                case LiftCommandType.WorldX:
                    AnimationSeq.Append(
                        transform.DOMoveX(
                            command.Distance,
                            command.Time));
                    break;
                case LiftCommandType.WorldY:
                    AnimationSeq.Append(
                        transform.DOMoveY(
                            command.Distance,
                            command.Time));
                    break;
                case LiftCommandType.WorldZ:
                    AnimationSeq.Append(
                        transform.DOMoveZ(
                            command.Distance,
                            command.Time));
                    break;
                default:
                    break;
            }
            // 检测障碍物
            AnimationSeq.OnUpdate(CheckObstacle);
            AnimationSeq.Play();

        }

        /// <summary>
        /// 利用射线检测是否有障碍物
        /// </summary>
        protected virtual bool HasObstacle()
        {
            var ray = new Ray(rayPosition, transform.right);
            if (!Physics.Raycast(ray, out var hit, RaycastDistance)) return false;
            
            var obstacle = hit.collider.gameObject;
            // 检测为其他机器人 自动避障
            if (obstacle.CompareTag("Lift")) return  true;
            return false;
        }

        // 取货
        protected virtual void PickUp()
        {
            
        }

        /// <summary>
        /// 机器人运动时持续检测障碍物
        /// </summary>
        protected virtual void CheckObstacle()
        {
            if (isAnimationPaused || !HasObstacle()) return;
            isAnimationPaused = true;
            AnimationSeq.Pause();
            Efficiency.Instance.taskAssignTime += ResumeTime;
            StartCoroutine(ResumeAnimation(ResumeTime));
        }
        
        /// <summary>
        /// 暂停机器人运动
        /// </summary>
        private IEnumerator ResumeAnimation(float delay)
        {
            yield return new WaitForSeconds(delay);
            AnimationSeq.Play();
            isAnimationPaused = false;
        }
        
        /// <summary>
        /// 销毁
        /// </summary>
        private void Destroy()
        {
            Stop();
            TaskManager.Instance.baseTaskAllocator.RemoveLift(liftId);
            BaseLift.liftCurCount--;
            Statistics.Instance.taskReady++;
            Destroy(gameObject);
        }
        
        protected  virtual void OnDestroy()
        {
            Stop();
            AnimationSeq.Kill();
        }

        /// <summary>
        /// 停止 WebSocket
        /// </summary>
        protected void Stop()
        {
            isAllowSend = false;
            isAllowRev = false;
        }
        
    }
}