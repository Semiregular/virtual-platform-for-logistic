using TMPro;
using UnityEngine;

namespace UI.Metrics
{
    public class Efficiency : MonoBehaviour
    {
        public GameObject tol;
        public GameObject perBox;
        public GameObject wait;
        public GameObject taskAssign;

        private TextMeshProUGUI tolTimeText;
        private TextMeshProUGUI timePerBoxText;
        private TextMeshProUGUI waitTimeText;
        private TextMeshProUGUI taskAssignTimeText;

        public float tolTime;
        public float waitTime;
        public float taskAssignStartTime;
        public float taskAssignTime;
        
        
        
        public static Efficiency Instance;
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
            tolTimeText = tol.GetComponentInChildren<TextMeshProUGUI>();
            timePerBoxText = perBox.GetComponentInChildren<TextMeshProUGUI>();
            waitTimeText = wait.GetComponentInChildren<TextMeshProUGUI>();
            taskAssignTimeText = taskAssign.GetComponentInChildren<TextMeshProUGUI>();
        }
        
        private void Update()
        {
            TolTime();
            WaitTime();
            TimePerBox();
            //TaskAssignTime();
        }

        private void TolTime()
        {
            tolTime += Time.deltaTime;
            var minute = tolTime / 60;
            var second = tolTime % 60;
            tolTimeText.text = minute.ToString("F0") + ":" + second.ToString("F0");
        }
        
        private void WaitTime()
        {
            var p = waitTime / tolTime * 100;
            waitTimeText.text = p.ToString("F2") + "%";
        }
        
        private void TimePerBox()
        {
            if (Statistics.Instance.boxReady == 0)
            {
                timePerBoxText.text = "Infinite";
                return;
            }
            var p = tolTime / Statistics.Instance.boxReady;
            timePerBoxText.text = p.ToString("F2") + "s";
        }

        private void TaskAssignTime()
        {
           taskAssignTimeText.text = taskAssignTime.ToString("F2") + "s";
        }

        public void TaskAssignStart()
        {
            taskAssignStartTime = tolTime;
        }
        
        public void TaskAssignEnd()
        {
            var i = tolTime - taskAssignStartTime;
            taskAssignTime += i;
            taskAssignTime /= Statistics.Instance.taskAssign;
        }
        
        public void Reset()
        {
            tolTime = 0;
            waitTime = 0;
            taskAssignTime = 0;
            taskAssignStartTime = 0;
        }
    }
}