using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Metrics
{
    public class Profiler : MonoBehaviour
    {
        public GameObject fps;
        public GameObject cpu;
        public GameObject gpu;
        public GameObject memory;
        public GameObject os;
        
        private TextMeshProUGUI fpsText;
        private TextMeshProUGUI cpuText;
        private TextMeshProUGUI gpuText;
        private TextMeshProUGUI memoryText;
        private TextMeshProUGUI osText;
        
        private const float SmoothDeltaTime = 0.2f;
        private float deltaTimeSum = 0f;
        private int frameCount = 0;
        public static Profiler Instance { get; private set; }

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
            fpsText = fps.GetComponent<TextMeshProUGUI>();
            cpuText = cpu.GetComponent<TextMeshProUGUI>();
            gpuText = gpu.GetComponent<TextMeshProUGUI>();
            memoryText = memory.GetComponent<TextMeshProUGUI>();
            osText = os.GetComponent<TextMeshProUGUI>();
            
            DeviceInfo();
        }

        private void Update()
        {
            Fps();
        }

        private void Fps()
        {
            deltaTimeSum += Time.deltaTime;
            frameCount++;

            if (deltaTimeSum > SmoothDeltaTime)
            {
                var fpsData = frameCount / deltaTimeSum;
                fpsText.text = fpsData.ToString("F2");

                deltaTimeSum = 0f;
                frameCount = 0;
            }
        }

        private void DeviceInfo()
        {
            cpuText.text = $"{SystemInfo.processorType}";
            gpuText.text= $"{SystemInfo.graphicsDeviceName}";
            osText.text = SystemInfo.operatingSystem;
            memoryText.text = $"{(int)(SystemInfo.systemMemorySize / 1024f / 1024f)}MB";
        }
    }

}
