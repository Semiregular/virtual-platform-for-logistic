using Newtonsoft.Json;

namespace Util
{
    public class NewJsonUtil
    {
        static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto, 
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public static string ToJson(object obj)
        {
            return  JsonConvert.SerializeObject(obj, Settings);
        }

        public static T FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, Settings);
        }
    }
    
}