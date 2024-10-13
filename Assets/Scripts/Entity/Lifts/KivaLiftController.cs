using DG.Tweening;
using Entity.Stacks;

namespace Entity.Lifts
{
    public class KivaLiftController : BaseLiftController
    {
        // 货架
        public Stack Stack
        {
            get; set;
        }
        // 是否完成
        public bool IsReady
        {
            get;
            set;
        }
        
        protected  override void InitProperties()
        {
           IsReady = false;
        }
        
        protected override void InitRayHeight()
        {
            if (Stack != null)
            {
                RayHeight = Stack.StackGameObject.transform.position.y;
            }
        }
        
        protected override void PickUp()
        {
            if(IsReady) return;
            var stackGameObject = Stack.StackGameObject;
            isPickUp = true;
            var pos = transform.position;
            // 取货高度
            pos.y = 0.5f;
            
            var stackAnimationSeq = DOTween.Sequence();
            stackAnimationSeq.Append(stackGameObject.transform.DOMove(pos, 1f));
            stackAnimationSeq.onComplete = () =>
            {
                IsReady = true;
                stackGameObject.transform.SetParent(transform);
                isPickUp = false;
            };
            stackAnimationSeq.Play();
        }
        
        protected override void OnDestroy()
        {
            Stop();
            AnimationSeq.Kill();
            KivaLift.liftCurCount--;
        }
    }
}