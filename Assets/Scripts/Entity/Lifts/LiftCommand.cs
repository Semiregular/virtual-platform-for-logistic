using Newtonsoft.Json;

namespace Entity.Lifts
{
    public class LiftCommand
    {
        public LiftCommand(LiftCommandType type, float distance, float time, int id)
        {
            Type = type;
            Distance = distance;
            Time = time;
            Id = id;
        }
        
        public LiftCommandType Type { get; set; }

        public float Distance { get; set; }
        
        public float Time { get; set; }
        
        public int Id { get; set; }
        
        
    }
}