
using System.Collections.Generic;
using Config;
using Manager;
using Entity.Objs;
using Entity.Stacks;
using Unity.VisualScripting;
using UnityEngine;

namespace Spawner.Stacks
{
    public class BaseStackSpawner : MonoBehaviour
    {
        public GameObject verticalLayerPrefab;
        public GameObject horizontalLayerPrefab;
        public GameObject boxPrefab;

        public Transform stackParent;
        public Transform stackOrigin;

        private GameObject stackPrefab;
        
        protected StackConfig StackConfig;
        protected BoxConfig BoxConfig;
        
        protected StackGroupConfig StackGroupConfig;
        protected MapConfig MapConfig;
        protected Vector3 stackOldOrigin;
        
        protected virtual void Start()
        {
            stackOldOrigin = new Vector3(stackOrigin.position.x,
                stackOrigin.position.y,
                stackOrigin.position.z);
            Init();
        }
        
        public  virtual void Init()
        {
            Config();
            Model();
            ReModel();
            ReOrigin();
            Spawn();
        }

        protected virtual void Model()
        {
            if (boxPrefab == null) return;
            // 生成stack物体
            var verticalLayerRight = Instantiate(
                verticalLayerPrefab, 
                Vector3.right * (StackConfig.StackLayerSize.x - StackConfig.StackVerticalSize) / 2 +
                Vector3.up * StackConfig.StackLayerSize.y / 2, 
                Quaternion.identity);
            
            var verticalLayerLeft = Instantiate(
                verticalLayerPrefab,
                Vector3.left * (StackConfig.StackLayerSize.x - StackConfig.StackVerticalSize) / 2 +
                Vector3.up * StackConfig.StackLayerSize.y / 2,
                Quaternion.identity);

            var horizontalLayer = Instantiate(
                horizontalLayerPrefab, 
                Vector3.up * StackConfig.StackHorizontalSize / 2, 
                Quaternion.identity);
            
            // 调整位置
            verticalLayerRight.transform.localScale = new Vector3(
                StackConfig.StackVerticalSize,
                StackConfig.StackLayerSize.y,
                StackConfig.StackLayerSize.z
            );

            verticalLayerLeft.transform.localScale = new Vector3(
                StackConfig.StackVerticalSize,
                StackConfig.StackLayerSize.y,
                StackConfig.StackLayerSize.z
            );

            horizontalLayer.transform.localScale = new Vector3(
                StackConfig.StackLayerSize.x- 2 * StackConfig.StackVerticalSize, 
                StackConfig.StackHorizontalSize,
                StackConfig.StackLayerSize.z
            );
            
            // 拼接为一层
            var stackLayerPrefab = new GameObject("Layer");
            var stackLayer = Instantiate(stackLayerPrefab, Vector3.zero, Quaternion.identity);
            Destroy(stackLayerPrefab);
            verticalLayerRight.transform.parent = stackLayer.transform;
            verticalLayerLeft.transform.parent = stackLayer.transform;
            horizontalLayer.transform.parent = stackLayer.transform;
            
            // 生成box原点和参数
            var boxOriginX = (BoxConfig.BoxLayout.x * BoxConfig.BoxSize.x +
                                (BoxConfig.BoxLayout.x - 1) * BoxConfig.BoxSpacing.x) / 2 -
                               BoxConfig.BoxSize.x / 2;
            var boxOriginZ = (BoxConfig.BoxLayout.z * BoxConfig.BoxSize.z +
                                (BoxConfig.BoxLayout.z - 1) * BoxConfig.BoxSpacing.z) / 2 -
                               BoxConfig.BoxSize.z / 2;
            var boxOriginY = BoxConfig.BoxSize.y / 2 + StackConfig.StackHorizontalSize;
            var boxOrigin = new Vector3((-1) * boxOriginX, boxOriginY, (-1) * boxOriginZ);
            
            var boxSpacingX = Vector3.right * (BoxConfig.BoxSize.x + BoxConfig.BoxSpacing.x);
            var boxSpacingZ = Vector3.forward * (BoxConfig.BoxSize.z + BoxConfig.BoxSpacing.z);
            
            // 生成一层box物体
            var oneBox = Instantiate(boxPrefab, boxOrigin, Quaternion.identity);
            oneBox.transform.localScale = new Vector3(
                BoxConfig.BoxSize.x,
                BoxConfig.BoxSize.y,
                BoxConfig.BoxSize.z
            );

            for(var i  = 0; i < BoxConfig.BoxLayout.x; i++)
            {
                for(var j = 0; j < BoxConfig.BoxLayout.z; j++)
                {
                    var pos = boxOrigin + i * boxSpacingX + j * boxSpacingZ;
                    var boxItem = Instantiate(oneBox,pos,Quaternion.identity);
                    boxItem.tag = "Box";
                    boxItem.transform.parent = stackLayer.transform;
                }
            }
            Destroy(oneBox);
            
            // 生成stack的所有层
            var stackPrefabTmp = new GameObject("Stack");
            var stack = Instantiate(stackPrefabTmp, Vector3.zero, Quaternion.identity);
            Destroy(stackPrefabTmp);
            for(var i = 0; i < StackConfig.StackLayerNum; i++)
            {
                var layer = Instantiate(stackLayer, Vector3.up * StackConfig.StackLayerSize.y * i, Quaternion.identity);
                layer.transform.parent = stack.transform;
            }
            Destroy(stackLayer);
            
            stackPrefab = stack;
            Destroy(stack);
        }

        protected virtual void Spawn()
        {
            if (stackPrefab == null) return;
            
            // 生成全部stack
            var x = stackOrigin.position.x;
            var z = stackOrigin.position.z;
            var groupX = 0;
            var groupZ = 0;

            while (x < MapConfig.MapSize.x)
            {
                while (z < MapConfig.MapSize.z)
                {
                    var pos = new Vector3(x, StackConfig.StackBottomHeight, z);
                    var stackTransform = Instantiate(stackPrefab, pos, Quaternion.identity);
                    stackTransform.transform.parent = stackParent;

                    DataBind(stackTransform);

                    groupZ = (groupZ + 1) % StackGroupConfig.StackGroupLayout.z;
                    if (groupZ == 0)
                    {
                        z += StackGroupConfig.StackGroupSpacing.z;
                    }
                    z += StackConfig.StackLayerSize.z + StackConfig.StackSpacing.z;
                }

                z = stackOrigin.position.z;
                groupZ = 0;

                groupX = (groupX + 1) % StackGroupConfig.StackGroupLayout.x;
                if (groupX == 0)
                {
                    x += StackGroupConfig.StackGroupSpacing.x;
                }
                x += StackConfig.StackLayerSize.x + StackConfig.StackSpacing.x;
            }
        }

        protected virtual void ReModel()
        {

        }

        protected virtual void ReOrigin()
        {
            if(stackOrigin == null) return;
            stackOrigin.position = stackOldOrigin;
            stackOrigin.position +=  Vector3.right * 
                                    (1 + (StackConfig.StackSpacing.x + StackConfig.StackLayerSize.x) / 2);
            stackOrigin.position += Vector3.forward * 
                                    (1 + (StackConfig.StackSpacing.z + StackConfig.StackLayerSize.z) / 2);
        }
        
        protected  virtual void Config()
        {
        }

        protected virtual void DataBind(GameObject stackTransform)
        {
            var stackEntity = new Stack(StackConfig.StackType ,stackTransform);
            StackManager.Instance.StackList.Add(stackEntity);
            for (var i = 0; i < stackTransform.transform.childCount; i++)
            {
                var layer = stackTransform.transform.GetChild(i);
                for (var j = 0; j < layer.transform.childCount; j++)
                {
                    var box = layer.transform.GetChild(j);
                    if(!box.CompareTag("Box")) continue;
                    var boxEntity = new Box(stackEntity.StackId, box.gameObject);
                    ObjManager.Instance.BoxList.Add(boxEntity);
                    ObjManager.Instance.AddObjMap(boxEntity);
                }
            }
        }

        public virtual void Destroy()
        {
            for (var i = stackParent.childCount - 1; i >= 0; i--)
            {
                var child = stackParent.GetChild(i).gameObject;
                Destroy(child);
            }
            Stack.stackCount = 0;
        }
    }

}
