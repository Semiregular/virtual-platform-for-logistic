namespace Spawner.Lifts
{
    public class KivaLiftSpawner : BaseLiftSpawner
    {
        public static KivaLiftSpawner Instance { get; private set; }
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