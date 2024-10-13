using System;
using Config;
using Entity.Stacks;
using Spawner.Lifts;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Assertions;

namespace Entity.Lifts
{

    public class KivaLift : BaseLift
    {

        [JsonProperty]
        public Stack Stack
        {
            get;
            set;
        }

        [JsonProperty]
        public bool IsReady
        {
            get;
            set;
        }
        
        public static int kivaLiftCount;
        
        
        public KivaLift() 
        {
            LiftId = liftCount++;
            LiftType = LiftType.Kiva;
            LiftGameObject = KivaLiftSpawner.Instance.Spawn();
            LiftController = LiftGameObject.GetComponent<KivaLiftController>();
            LiftPosition = LiftGameObject.transform.position;
            IsReady = false;
            kivaLiftCount++;
        }
        
        public KivaLift(Stack stack) 
        {
            LiftId = liftCount % GlobalConfig.liftNum;
            liftCount++;
            liftCurCount++;
            IsRemoved = false;
            
            LiftType = LiftType.Kiva;
            Stack = new Stack(stack);
            LiftGameObject = KivaLiftSpawner.Instance.Spawn();
            LiftController = LiftGameObject.GetComponent<KivaLiftController>();
            
            var kivaLiftController = (KivaLiftController)LiftController;
            kivaLiftController.Stack = Stack;
            kivaLiftController.liftId = LiftId;
            
            LiftPosition = LiftGameObject.transform.position;
            IsReady = false;
            kivaLiftCount++;
            
            
        }
        
        public override void UpdatePosition()
        {
            LiftPosition = LiftGameObject.transform.position;
            LiftDirection = LiftGameObject.transform.forward;
            IsPaused = LiftController.isAnimationPaused;
            IsPickUp = LiftController.isPickUp;
            
            var kivaLiftController = (KivaLiftController)LiftController;
            IsReady = kivaLiftController.IsReady;
        }
        
        public static void Clear()
        {
            liftCurCount = 0;
            liftCount = 0;
            kivaLiftCount = 0;
        }
    }
}