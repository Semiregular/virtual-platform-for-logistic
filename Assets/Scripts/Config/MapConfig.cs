using UnityEngine;

namespace Config
{
    public class MapConfig
    {
        public MapConfig(Vector3 mapSize, float mapFrictionStatic, float mapFrictionKinetic)
        {
            MapSize = mapSize;
            MapFrictionStatic = mapFrictionStatic;
            MapFrictionKinetic = mapFrictionKinetic;
        }

        public Vector3 MapSize
        {
            get;
            set;
        }

        public float MapFrictionStatic
        {
            get;
            set;
        }
        
        public float MapFrictionKinetic
        {
            get;
            set;
        }
    }
}