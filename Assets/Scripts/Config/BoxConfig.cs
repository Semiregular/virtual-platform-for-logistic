using UnityEngine;

namespace Config
{
    public class BoxConfig
    {
        public BoxConfig(Vector3 boxSize, Vector3 boxSpacing, Vector3Int boxLayout)
        {
            BoxSize = boxSize;
            BoxSpacing = boxSpacing;
            BoxLayout = boxLayout;
        }
        
        public Vector3 BoxSize
        {
            get ;
            set ;
        }

        public Vector3 BoxSpacing
        {
            get ;
            set ;
        }

        public Vector3Int BoxLayout
        {
            get;
            set;
        }
        
    }
}
    
