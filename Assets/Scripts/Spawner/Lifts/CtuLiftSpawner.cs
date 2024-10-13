

namespace Spawner.Lifts
{
    public class CtuLiftSpawner : BaseLiftSpawner
    {
        public static CtuLiftSpawner Instance { get; private set; }
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
    }
}