using Config;
using Newtonsoft.Json;
using UnityEngine;

namespace Entity.Lifts
{
    public class BaseLift 
    {
        // 只增不减
        public static int liftCount = 0;
        // 当前数量
        public static int liftCurCount;

        protected BaseLift()
        {
            LiftId = liftCount % GlobalConfig.liftNum;
            liftCount++;
            liftCurCount++;
            IsRemoved = false;
        }
        
        [JsonProperty]
        public int LiftId { get; set; }
        
        [JsonProperty]
        public LiftType LiftType { get; set; }
        
        [JsonProperty]
        public Vector3 LiftPosition { get; set; }
        
        [JsonProperty]
        public Vector3 LiftDirection { get; set; }
        
        [JsonProperty]
        public bool IsPaused { get; set; }
        
        [JsonProperty]
        public bool IsPickUp { get; set; }
        
        [JsonIgnore]
        public GameObject LiftGameObject { get; set; }
        
        [JsonIgnore]
        public BaseLiftController LiftController { get; set; }

        [JsonIgnore]
        public bool IsRemoved
        {
            get;
            set;
        }

        public virtual void UpdatePosition()
        {
            LiftPosition = LiftGameObject.transform.position;
            LiftDirection = LiftGameObject.transform.forward;
            IsPaused = LiftController.isAnimationPaused;
            IsPickUp = LiftController.isPickUp;
        }

        public bool IsValid()
        {
            return LiftGameObject != null && LiftController != null;
        }
        
    }
}

