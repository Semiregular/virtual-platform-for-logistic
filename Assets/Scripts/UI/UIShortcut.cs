using UnityEngine;

namespace UI
{
    public class UIShortcut : MonoBehaviour
    {

        public GameObject settingCanvas;
        public GameObject metricCanvas;
        public GameObject toolbarCanvas;
        
        private bool isPause = false;

        private void Start()
        {
            settingCanvas.SetActive(false);
            metricCanvas.SetActive(true);
            toolbarCanvas.SetActive(true);
        }

        private void Update()
        {
        
            if (Input.GetKeyDown(KeyCode.E))
            {
                Pause();
                settingCanvas.SetActive(!settingCanvas.activeSelf);
                metricCanvas.SetActive(false);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                metricCanvas.SetActive(!metricCanvas.activeSelf);
            }
        
        }

        private void Pause()
        {
            isPause = !isPause;
            Time.timeScale = isPause ? 0 : 1;
        }
    }
}

