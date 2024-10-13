namespace Config
{
    public class ObjConfig
    {
        public ObjConfig(int objCategory, int objType)
        {
            ObjCategory = objCategory;
            ObjType = objType;
        }

        public int ObjCategory
        {
            get;
            set;
        }

        public int ObjType
        {
            get;
            set;
        }
    }
}