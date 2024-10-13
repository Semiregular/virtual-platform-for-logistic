using System;
using System.Collections.Generic;
using Config;
using Entity.Objs;
using Spawner.Lifts;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Assertions;

namespace Entity.Lifts
{
    public class CtuLift : BaseLift
    {
        [JsonProperty]
        public List<Box> BoxNotReady { get; set; }
        
        [JsonIgnore]
        public List<Box> BoxReady { get; set; }
        
        public static int ctuLiftCount;
        
        public CtuLift()
        {
            LiftType = LiftType.Ctu;
            LiftGameObject = CtuLiftSpawner.Instance.Spawn();
            LiftController = LiftGameObject.GetComponent<CtuLiftController>();
            LiftPosition = LiftGameObject.transform.position;
            BoxReady = new List<Box>();
            BoxNotReady = new List<Box>();
            ctuLiftCount++;
        }

        public CtuLift(List<Box> boxNotReady) 
        {
            LiftId = liftCount % GlobalConfig.liftNum;
            liftCount++;
            liftCurCount++;
            IsRemoved = false;
            
            LiftType = LiftType.Ctu;
            LiftGameObject = CtuLiftSpawner.Instance.Spawn();
            LiftController = LiftGameObject.GetComponent<CtuLiftController>();
            BoxNotReady = new List<Box>(boxNotReady);
            BoxReady = new List<Box>();
            
            var ctuLiftController = (CtuLiftController)LiftController;
            ctuLiftController.BoxNotReady = BoxNotReady;
            ctuLiftController.liftId = LiftId;
            
            LiftPosition = LiftGameObject.transform.position;
            ctuLiftCount++;
            
        }
        
        public override void UpdatePosition()
        {
            LiftPosition = LiftGameObject.transform.position;
            LiftDirection = LiftGameObject.transform.forward;
            IsPaused = LiftController.isAnimationPaused;
            IsPickUp = LiftController.isPickUp;
            
            var ctuLiftController = (CtuLiftController)LiftController;
            BoxNotReady = ctuLiftController.BoxNotReady;
        }

        public static void Clear()
        {
            liftCurCount = 0;
            liftCount = 0;
            ctuLiftCount = 0;
        }
    }
}