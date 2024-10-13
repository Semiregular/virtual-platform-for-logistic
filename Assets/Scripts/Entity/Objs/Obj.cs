using System;
using Config;

namespace Entity.Objs
{
    public class Obj
    {
        public int ObjCategory
        {
            get ;
            set ;
        }

        public string ObjName
        {
            get ;
            set ;
        }

        public int ObjType
        {
            get ;
            set ;
        }

        private static readonly ObjConfig ObjConfig = GlobalConfig.objConfig;
        
        public Obj(int objCategory, string objName, int objType)
        {
            ObjCategory = objCategory;
            ObjName = objName;
            ObjType = objType;
        }


        public Obj()
        {
            ObjCategory = UnityEngine.Random.Range(0, ObjConfig.ObjCategory);
            ObjName = Guid.NewGuid().ToString();
            ObjType = UnityEngine.Random.Range(0, ObjConfig.ObjType);
        }


    }

}
