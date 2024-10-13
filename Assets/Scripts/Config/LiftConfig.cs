namespace Config
{
    public class LiftConfig
    {
        public LiftConfig(int liftLayerNum, float liftSpeed)
        {
            LiftLayerNum = liftLayerNum;
            LiftSpeed = liftSpeed;
        }

        public int LiftLayerNum
        {
            get;
            set;
        }

        public float LiftSpeed
        {
            get;
            set;
        }
    };
}