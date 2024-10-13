using System;
using System.Collections.Generic;
using Config;
using Entity.Objs;
using Util;

namespace Entity.Tasks
{
    public class Task 
    {
        public static int taskCount;
        private static TaskConfig taskConfig = GlobalConfig.taskConfig; 
        private static readonly BitmapAllocator BitmapAllocator = new(taskConfig.TaskNum);

        private int taskId;
        private string taskName;
        private long taskCreate;
        private List<Obj> objList;

        public Task(int taskId, string taskName, long taskCreate, List<Obj> objList)
        {
            TaskId = taskId;
            TaskName = taskName;
            TaskCreate = taskCreate;
            ObjList = objList;
        }

        public Task()
        {
            TaskId = BitmapAllocator.Allocate();
            TaskName = Guid.NewGuid().ToString();
            TaskCreate = TimeUtil.GetTolSeconds();

            var l = UnityEngine.Random.Range(0, taskConfig.TaskObjSize);
            ObjList = new List<Obj>(l);
            for (var i = 0; i < l; i++)
            {
                Obj obj = new();
                ObjList.Add(obj);
            }
        }

        public int TaskId { get ; set ; }
        public string TaskName { get ; set ; }
        public long TaskCreate { get ; set ; }
        public List<Obj> ObjList { get ; set ; }
        
        public static  void Clear()
        {
            taskCount = 0;
        }
    }

}
