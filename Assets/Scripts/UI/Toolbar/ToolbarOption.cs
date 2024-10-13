using Entity.Lifts;
using Entity.Stacks;
using Entity.Tasks;
using Manager;
using Spawner;
using Spawner.Lifts;
using Spawner.Stacks;
using UI.Metrics;
using UnityEngine;
using Box = Entity.Objs.Box;


namespace UI.Toolbar
{
    public class ToolbarOption : MonoBehaviour
    {
        public GameObject pauseBg;
        public GameObject debugBg;
        public GameObject clearBg;
        
        private static bool _isPause;
        private static bool _isRun;
        public static ToolbarOption Instance { get; private set; }

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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Pause();
            }

            if (!_isRun && Input.GetKeyDown(KeyCode.R))
            {
                Run();
            }
        }
        
        /// <summary>
        /// 暂停运行或者继续运行
        /// </summary>
        public void Pause()
        {
            _isPause = !_isPause;
            Time.timeScale = _isPause ? 0 : 1;
        }
        
        /// <summary>
        /// 单步运行
        /// </summary>
        public void Debug()
        {
            _isPause = true;
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.timeScale / 50f;
            Invoke("ResetTimeScale", 0.02f); 
        }
        
        /// <summary>
        /// 暂停运行，时间缩放为0
        /// </summary>
        private void ResetTimeScale()
        {
            Time.timeScale = 0f; 
        }
        
        /// <summary>
        /// 清空重来
        /// </summary>
        public void Clear()
        {
            if (!_isRun)
            {
                Run();
                return;
            }
            _isRun = false;
            // 关闭任务
            TaskManager.Instance.baseTaskAllocator.Stop();
            // 销毁所有对象
            CtuStackSpawner.Instance.Destroy();
            KivaStackSpawner.Instance.Destroy();
            CtuLiftSpawner.Instance.Destroy();
            KivaLiftSpawner.Instance.Destroy();
            ArucoSpawner.Instance.Destroy();
            // 清空数据
            ObjManager.Instance.ObjMap.Clear();
            ObjManager.Instance.BoxList.Clear();
            StackManager.Instance.StackList.Clear();
            Task.Clear();
            Stack.Clear();
            Box.Clear();
            CtuLift.Clear();
            KivaLift.Clear();
            // 重置统计信息
            Statistics.Instance.Reset();
            Efficiency.Instance.Reset();
            
            Init();
        }
    
        private static void Init()
        {
            ObjManager.Instance.Init();
            CtuStackSpawner.Instance.Init();
            KivaStackSpawner.Instance.Init();
            ArucoSpawner.Instance.Init();
        }

        private static void Run()
        {
            _isRun = true;
            TaskManager.Instance.baseTaskAllocator.Run();
        }
    }
}

