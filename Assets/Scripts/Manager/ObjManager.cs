using System.Collections.Generic;
using Config;
using Entity.Objs;
using Entity.Stacks;
using UnityEngine;

namespace Manager
{
    public class ObjManager : MonoBehaviour
    {
        public static ObjManager Instance { get; private set; }

        public List<List<ObjState>> ObjMap;
        public List<Box> BoxList;
        
        private readonly object objMapLock = new ();
        private readonly object boxListLock = new ();
        
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

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            ObjMap = new List<List<ObjState>>();
            for (var i = 0; i < GlobalConfig.objConfig.ObjCategory; i++)
            {
                ObjMap.Add(new List<ObjState>());
            }
            BoxList = new List<Box>();
        }
        

        public void AddObjMap(Box box)
        {
            var objStateList = ObjMap[box.Obj.ObjCategory];
            var isExist = false;
            objStateList.ForEach(objState =>
            {
                if (objState.StackId == box.StackId)
                {
                    objState.ObjNum += box.ObjNum;
                    objState.BoxList.Add(box);
                    isExist = true;
                }
            });

            if (!isExist)
            {
                objStateList.Add(new ObjState(box.StackId, box.ObjNum, box));
            }
        }

        public void RemoveObjMap(Box box)
        {
            var objStateList = ObjMap[box.Obj.ObjCategory];
            var  r = new List<ObjState>();
                objStateList.ForEach(objState =>
                {
                    if (objState.StackId == box.StackId)
                    {
                        objState.ObjNum -= box.ObjNum;
                        objState.BoxList.Remove(box);
                    
                        if (objState.ObjNum <= 0)
                        {
                            r.Add(objState);
                        }
                    }
                });
            if (r.Count > 0)
            {
                objStateList.RemoveAll(objState => r.Contains(objState));    
            }
        }

        public void RemoveStack(Stack stack)
        {
           
            ObjMap.ForEach(objStateList =>
            {
                objStateList.RemoveAll(objState => objState.StackId == stack.StackId);
            });
            
            BoxList.RemoveAll(box => box.StackId == stack.StackId);
            StackManager.Instance.StackList.Remove(stack);
        }
    }
}

