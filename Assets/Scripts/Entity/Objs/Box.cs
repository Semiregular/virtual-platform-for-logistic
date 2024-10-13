using Newtonsoft.Json;
using UnityEngine;

namespace Entity.Objs
{
    public class Box
    {
        [JsonProperty]
        public Vector3 BoxPosition
        {
            get ;
            set ;
        }

        [JsonProperty]
        public int BoxId
        {
            get ;
            set ;
        }

        [JsonIgnore]
        public int StackId
        {
            get ;
            set ;
        }

        [JsonIgnore]
        public Obj Obj
        {
            get ;
            set ;
        }

        [JsonIgnore]
        public int ObjNum
        {
            get ;
            set ;
        }

        [JsonIgnore]
        public GameObject BoxGameObject
        {
            get ;
            set ;
        }

        private static int _boxCount = 0;
        
        public Box(int stackId, GameObject box)
        {
            BoxId = _boxCount++;
            Obj = new Obj();
            ObjNum = 20;
            BoxGameObject = box;
            BoxPosition = box.transform.position;
            StackId = stackId;
        }

        public Box(Box box)
        {
            BoxId = box.BoxId;
            Obj = box.Obj;
            ObjNum = box.ObjNum;
            BoxGameObject = box.BoxGameObject;
            BoxPosition = box.BoxPosition;
            StackId = box.StackId;
        }

        public static void Clear()
        {
            _boxCount = 0;
        }
    }
}

