using System;
using System.Collections;
using System.Collections.Generic;
using Config;
using Entity.Lifts;
using Entity.Objs;
using Entity.Tasks;
using Manager;
using UI.Metrics;
using UnityEngine;

namespace Algo.Random
{
    public class RandomTaskAllocator : BaseTaskAllocator
    {

        public static RandomTaskAllocator Instance { get; private set; }
        
        private const int MaxProb= 100;
        private const int MagicProb= 20;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
        
        protected override IEnumerator TaskAutoAssign()
        {
            while (GlobalConfig.isTaskAutoAssign && IsAllowAssign)
            {
                Task task = null;
                // 从任务队列中取出一个任务
                if (taskQueue.Count > 0 && BaseLift.liftCount < GlobalConfig.liftNum)
                {
                    taskQueue.TryDequeue(out task);
                }
                
                try
                {
                    if (task != null)
                    {
                        Statistics.Instance.taskAssign++;
                        var objList = task.ObjList;
                        var boxList = new List<Box>();
                        // 分配要取的 box
                        foreach (var obj in objList)
                        {
                            // 货物种类
                            var stackList = ObjManager.Instance.ObjMap[obj.ObjCategory];
                            if (stackList.Count > 0)
                            {
                                // 随机货架的第一个 box
                                var index = UnityEngine.Random.Range(0, stackList.Count);
                                var prob = UnityEngine.Random.Range(0, MaxProb);
                                if (prob < MagicProb)
                                {
                                    var stackId = stackList[index].StackId;
                                    var  stack = StackManager.Instance.StackList
                                        .Find(x => x.StackId == stackId);
                                    AddKivaLift(stack);
                                }
                                else
                                {
                                    var box = stackList[index].BoxList[0];
                                    boxList.Add(box);
                                }
                            }
                        }
                        if (boxList.Count > 0)
                        {
                            AddCtuLift(boxList);
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    throw;
                }
                
                yield return new WaitForSeconds(TaskConfig.TaskAssignSecond);
            }
            yield return null;
        }
    }
}

