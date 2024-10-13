
using Entity.Stacks;
using UnityEngine;

namespace Config
{
    public class StackConfig
    {
        public StackConfig(Vector3 stackLayerSize, 
            Vector3 stackSpacing, 
            int stackLayerNum, 
            float stackBottomHeight, 
            float stackVerticalSize, 
            float stackHorizontalSize,
            StackType stackType)
        {
            StackLayerSize = stackLayerSize;
            StackSpacing = stackSpacing;
            StackLayerNum = stackLayerNum;
            StackBottomHeight = stackBottomHeight;
            StackVerticalSize = stackVerticalSize;
            StackHorizontalSize = stackHorizontalSize;
            StackType = stackType;
        }

        public Vector3 StackLayerSize { get; set; }
        public Vector3 StackSpacing { get; set; }
        public int StackLayerNum { get; set ; }
        public float StackBottomHeight { get; set; }
        public float StackVerticalSize{ get; set; }
        public float StackHorizontalSize{ get; set; }
        public StackType StackType { get; set; }

    }
}

