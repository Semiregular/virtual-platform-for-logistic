using UnityEngine;

namespace Config
{
    public class ArucoConfig
    {
        public ArucoConfig(Vector3 arucoSize, Vector3 arucoSpacing)
        {
            ArucoSize = arucoSize;
            ArucoSpacing = arucoSpacing;
        }

        public Vector3 ArucoSize
        {
            get;
            set;
        }
        
        public Vector3 ArucoSpacing
        {
            get;
            set;
        }
    }
}