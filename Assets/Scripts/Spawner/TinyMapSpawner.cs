
using UnityEngine;
using System;
using Config;

namespace Spawner
{
    public class TinyMapSpawner : MonoBehaviour
    {
        public GameObject mapBlockPrefab;
        public GameObject kivaBlockPrefab;
        
        public RectTransform mapOrigin;
        public RectTransform mapContainer;
        public static TinyMapSpawner Instance { get; private set; }
        
        private readonly MapConfig mapConfig = GlobalConfig.mapConfig;
        private readonly TinyMapConfig tinyMapConfig = GlobalConfig.tinyMapConfig;
        private readonly StackConfig stackKiva = GlobalConfig.stackKiva;
        private readonly StackConfig stackCtu = GlobalConfig.stackCtu;
        private readonly StackGroupConfig stackGroupConfig = GlobalConfig.stackGroupConfig;

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
        void Start()
        {
            AdjustContainer();
            Spawn();
        }

        private void AdjustContainer()
        {
            var x = mapConfig.MapSize.x * (tinyMapConfig.MapBlockSize + tinyMapConfig.MapBlockSpacing);
            var y = mapConfig.MapSize.z * (tinyMapConfig.MapBlockSize + tinyMapConfig.MapBlockSpacing);
            mapContainer.sizeDelta = new Vector2(x,y);
        }

        private void Spawn()
        {
            
            var x = mapOrigin.position.x + tinyMapConfig.MapBlockSize / 2;
            var y = mapOrigin.position.y - tinyMapConfig.MapBlockSize / 2;

            var countX = 0;
            var countY = 0;

            while (countX < mapConfig.MapSize.x )
            {
                while (countY < mapConfig.MapSize.z)
                {
                    var pos = new Vector3(x, y, 0);

                    if(countX == 0 ||  countY == 0 )
                    {
                        var mapBlock = Instantiate(mapBlockPrefab, pos, Quaternion.identity);
                        mapBlock.transform.SetParent(mapOrigin);
                        countY++;
                        y -= tinyMapConfig.MapBlockSize + tinyMapConfig.MapBlockSpacing;
                        continue;
                    }

                    if(HasStack(countX - 1 , countY - 1, stackKiva))
                    {
                        var mapBlock = Instantiate(kivaBlockPrefab, pos, Quaternion.identity);
                        mapBlock.transform.SetParent(mapOrigin);
                    }
                    else
                    {
                        var mapBlock = Instantiate(mapBlockPrefab, pos, Quaternion.identity);
                        mapBlock.transform.SetParent(mapOrigin);
                    }
                    

                    countY++;
                    y -= tinyMapConfig.MapBlockSize + tinyMapConfig.MapBlockSpacing;
                }

                countX++;
                x += tinyMapConfig.MapBlockSize + tinyMapConfig.MapBlockSpacing;
                countY = 0;
                y = mapOrigin.position.y - tinyMapConfig.MapBlockSize / 2;
            }
        }

        private bool HasStack(int x, int z, StackConfig stackConfig)
        {
            var divX = (int)Math.Ceiling(stackGroupConfig.StackGroupLayout.x * stackConfig.StackLayerSize.x + 
                                         stackGroupConfig.StackGroupSpacing.z); 
            var rangeX = (int)Math.Ceiling(stackGroupConfig.StackGroupLayout.x * stackConfig.StackLayerSize.x);

            var divZ = (int)Math.Ceiling(stackGroupConfig.StackGroupLayout.z * stackConfig.StackLayerSize.z + 
                                         stackGroupConfig.StackGroupSpacing.x);
            var rangeZ = (int)Math.Ceiling(stackGroupConfig.StackGroupLayout.z * stackConfig.StackLayerSize.z);

            if(x % divX < rangeX && z % divZ < rangeZ) return true;
            return false;
        }
    }

}


