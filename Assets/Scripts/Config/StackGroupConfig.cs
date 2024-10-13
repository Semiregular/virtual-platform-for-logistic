using UnityEngine;

namespace Config
{
    public class StackGroupConfig
    {
        public StackGroupConfig(Vector3Int stackGroupLayout, 
                                Vector3 stackGroupSpacing)
        {
            StackGroupLayout = stackGroupLayout;
            StackGroupSpacing = stackGroupSpacing;
        }
    
        public Vector3Int StackGroupLayout { get; set; }
    
        public Vector3 StackGroupSpacing { get; set; }
        
    }
}
