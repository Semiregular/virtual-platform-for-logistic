using UnityEngine;

namespace Entity.Lifts
{
    [System.Serializable]
    public class LiftState
    {
        public LiftState(int liftId, Vector3 position, Vector3 direction, Vector3 velocity)
        {
            this.liftId = liftId;
            this.position = position;
            this.direction = direction;
            this.velocity = velocity;
        }
        

        public int liftId;
        public Vector3 position;
        public Vector3 direction;
        public Vector3 velocity;
        
        
    }

}