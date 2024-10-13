

using Newtonsoft.Json;
using UnityEngine;

namespace Entity.Stacks
{
    public class Stack
    {
        public static int stackCount;
        
        public Stack(int stackId, StackType stackType, GameObject stackGameObject)
        {
            StackId = stackId;
            StackType = stackType;
            StackGameObject = stackGameObject;
        }
        
        public Stack(StackType stackType, GameObject stackGameObject)
        {
            StackId = stackCount++;
            StackType = stackType;
            StackGameObject = stackGameObject;
            StackPosition = stackGameObject.transform.position;
        }

        public Stack(Stack stack)
        {
            StackId = stack.StackId;
            StackType = stack.StackType;
            StackGameObject = stack.StackGameObject;
            StackPosition = stack.StackPosition;
        }
        
        [JsonProperty]
        public int StackId { get ; set ; }
        
        [JsonIgnore]
        public StackType StackType { get ; set ; }
        
        [JsonIgnore]
        public GameObject StackGameObject { get ; set ; }
        
        [JsonProperty]
        public Vector3 StackPosition
        {
            get ;
            set ;
        }

        public static void Clear()
        {
            stackCount = 0;
        }
    }
}

