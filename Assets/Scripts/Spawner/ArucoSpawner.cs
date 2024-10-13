using Config;
using UnityEngine;

namespace Spawner
{
    public class ArucoSpawner : MonoBehaviour
    {
        public GameObject arucoPrefab;
        public Transform arucoOrigin;
        
        private const float ArucoHeight = 0.01f;
        public static ArucoSpawner Instance { get; private set; }
        
        private readonly MapConfig mapConfig = GlobalConfig.mapConfig;
        private readonly ArucoConfig arucoConfig = GlobalConfig.arucoConfig;
    
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
            Init();
        }

        public void Init()
        {
            Spawn();
        }
    
        private void Spawn()
        {
            var x = arucoOrigin.position.x;
            var z = arucoOrigin.position.z;
    
            var arucoRotation = arucoPrefab.transform.rotation;
    
            while (x <= mapConfig.MapSize.x)
            {
                while(z <= mapConfig.MapSize.z)
                {
                    var pos = new Vector3(x, ArucoHeight, z);
                    var aruco = Instantiate(arucoPrefab, pos, arucoRotation);
                    aruco.transform.SetParent(arucoOrigin);
    
                    z += arucoConfig.ArucoSpacing.z;
                }
    
                x += arucoConfig.ArucoSpacing.x;
                z = arucoOrigin.position.z;
            }
        }
    
        public void Destroy()
        {
            for (var i = arucoOrigin.childCount - 1; i >= 0; i--)
            {
                var child = arucoOrigin.GetChild(i).gameObject;
                Destroy(child);
            }
        }
    
    
    }

}
