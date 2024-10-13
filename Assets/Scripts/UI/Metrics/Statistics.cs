using System;
using Entity.Lifts;
using Entity.Tasks;
using TMPro;
using UnityEngine;

namespace UI.Metrics
{
    public class Statistics : MonoBehaviour
    {
        public GameObject box;
        public GameObject lift;
        public GameObject task;
        
        private TextMeshProUGUI boxText;
        private TextMeshProUGUI liftText;
        private TextMeshProUGUI taskText;
        
        public  int boxAssign;
        public  int boxReady;
        public  int taskAssign;
        public  int taskReady;
        
        public static Statistics Instance { get; private set; }
        
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
            boxText = box.GetComponentInChildren<TextMeshProUGUI>();
            liftText = lift.GetComponentInChildren<TextMeshProUGUI>();
            taskText = task.GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Update()
        {
            BoxStatistics();
            LiftStatistics();
            TaskStatistics();
        }

        private void BoxStatistics()
        {
            boxText.text = $"{boxReady}/{boxAssign}";
        }
        
        private void LiftStatistics()
        {
            liftText.text = $"{KivaLift.kivaLiftCount}/{CtuLift.ctuLiftCount}";
        }
        
        private void TaskStatistics()
        {
            taskText.text = $"{taskReady}/{taskAssign}";  
        }

        public void Reset()
        {
            boxAssign = 0;
            boxReady = 0;
            taskAssign = 0;
            taskReady = 0;
            KivaLift.kivaLiftCount = 0;
            CtuLift.ctuLiftCount = 0;
        }
    }
}