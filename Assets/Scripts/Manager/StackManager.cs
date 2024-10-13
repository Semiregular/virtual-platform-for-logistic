using System.Collections.Generic;
using Entity.Stacks;
using UnityEngine;

namespace Manager
{
    public class StackManager : MonoBehaviour
    {
        public static StackManager Instance { get; private set; }
        
        public List<Stack> StackList;
        
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

        private void Init()
        {
            StackList = new List<Stack>();
        }
    }
}