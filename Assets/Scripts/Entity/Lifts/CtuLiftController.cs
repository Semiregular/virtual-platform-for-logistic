using System.Collections.Generic;
using DG.Tweening;
using Config;
using Entity.Objs;
using UI.Metrics;
using UnityEngine;


namespace Entity.Lifts
{
    public class CtuLiftController : BaseLiftController
    {
        // 待取货的box
        public List<Box> BoxNotReady
        {
            get;
            set;
        }
        // 已取货的box
        public List<Box> BoxReady
        {
            get;
            set;
        }

        protected override void InitRayHeight()
        {
            if (BoxNotReady.Count > 0)
            {
                RayHeight = BoxNotReady[0].BoxPosition.y;
            }
        }
        
        protected  override void InitProperties()
        {
            BoxReady = new List<Box>();
        }

        protected override void PickUp()
        {
            if(BoxNotReady.Count == 0) return;
            var box = BoxNotReady[0];
            var boxGameObject = box.BoxGameObject;
            isPickUp = true;
            var pos = transform.position;
            // 取货高度
            pos.y = 5 + BoxReady.Count * GlobalConfig.boxCtu.BoxSize.y;
            // 取货动画
            var boxAnimationSeq = DOTween.Sequence();
            boxAnimationSeq.Append(boxGameObject.transform.DOMove(pos, 1f));
            // 回调函数
            boxAnimationSeq.onComplete = () =>
            {
                var boxReady = new Box(box);
                BoxReady.Add(boxReady);
                BoxNotReady.RemoveAt(0);
                Statistics.Instance.boxReady++;
                boxGameObject.transform.SetParent(transform);
                isPickUp = false;
                InitRayHeight();
            };
            boxAnimationSeq.Play();
        }

        protected override void OnDestroy()
        {
            Stop();
            AnimationSeq.Kill();
            CtuLift.liftCurCount--;
        }

    }
}