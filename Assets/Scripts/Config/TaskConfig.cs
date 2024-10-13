namespace Config
{
    public class TaskConfig
    {
        public TaskConfig(int taskNum, int taskObjSize, float taskIncSecond, float taskAssignSecond)
        {
            TaskNum = taskNum;
            TaskObjSize = taskObjSize;
            TaskIncSecond = taskIncSecond;
            TaskAssignSecond = taskAssignSecond;
        }

        public int TaskNum
        {
            get;
            set;
        }

        public int TaskObjSize
        {
            get;
            set;
        }

        public float TaskIncSecond
        {
            get;
            set;
        }

        public float TaskAssignSecond
        {
            get;
            set;
        }

    }
}