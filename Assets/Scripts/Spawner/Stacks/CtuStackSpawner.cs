using Config;

namespace Spawner.Stacks
{
    public class CtuStackSpawner : BaseStackSpawner
    {

        public static CtuStackSpawner Instance { get; private set; }

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
        
        protected override void Config()
        {
            StackConfig = GlobalConfig.stackCtu;
            BoxConfig = GlobalConfig.boxCtu;
            
            StackGroupConfig = GlobalConfig.stackGroupConfig;
            MapConfig = GlobalConfig.mapConfig;
        }
    
    }

}