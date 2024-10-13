using Algo.Random;
using UnityEngine;

namespace Entity.Tasks
{
    public class TaskManager : MonoBehaviour
    {
        public static TaskManager Instance { get; private set; }

        public BaseTaskAllocator baseTaskAllocator;
        
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
            baseTaskAllocator = gameObject.AddComponent<RandomTaskAllocator>();
        }
    }

}
