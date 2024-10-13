using System.Collections.Generic;

namespace Entity.Objs
{
    public class ObjState
    {
        public ObjState(int stackId, int objNum, Box box)
        {
            StackId = stackId;
            ObjNum = objNum;
            BoxList = new List<Box>();
            BoxList.Add(box);
        }

        public int StackId
        {
            get;
            set;
        }

        public int ObjNum
        {
            get;
            set;
        }

        public List<Box> BoxList
        {
            get;
            set;
        }
    }
}