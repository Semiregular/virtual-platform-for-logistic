using System.Collections.Generic;
using Entity.Lifts;
using Entity.Objs;
using Entity.Stacks;

namespace Entity.Tasks
{
    public class TaskCommand
    {
        public TaskCommand(LiftType type, List<int> boxNotReady, int stackId)
        {
            Type = type;
            BoxNotReady = boxNotReady;
            StackId = stackId;
        }

        public LiftType Type
        {
            get; set;
        }

        public List<int> BoxNotReady
        {
            get; set;
        }

        public int StackId
        {
            get; set;
        }
    }
}