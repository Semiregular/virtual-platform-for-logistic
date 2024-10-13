namespace Config
{
    public class TinyMapConfig
    {
        public TinyMapConfig(float mapBlockSize, float mapBlockSpacing)
        {
            MapBlockSize = mapBlockSize;
            MapBlockSpacing = mapBlockSpacing;
        }

        public float MapBlockSize
        {
            get;
            set;
        }
        public float MapBlockSpacing
        {
            get;
            set;
        }
    }
}