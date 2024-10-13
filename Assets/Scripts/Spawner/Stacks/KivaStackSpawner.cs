using Config;

namespace Spawner.Stacks
{
    public class KivaStackSpawner : BaseStackSpawner
    {
        public static KivaStackSpawner Instance { get; private set; }

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
            StackConfig = GlobalConfig.stackKiva;
            BoxConfig = GlobalConfig.boxKiva;
            
            StackGroupConfig = GlobalConfig.stackGroupConfig;
            MapConfig = GlobalConfig.mapConfig;
        }
    
    }

}
