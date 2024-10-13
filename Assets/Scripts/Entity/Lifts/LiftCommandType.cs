using Newtonsoft.Json;

namespace Entity.Lifts
{
    public enum LiftCommandType
    {
        Stop = 0,
        
        RelativeX = 1,
        RelativeY = 2,
        RelativeZ = 3,
        
        Turn = 4,
        Nothing = 5,
        
        WorldX = 6,
        WorldY = 7,
        WorldZ = 8,
        
        PickUp = 9,
        Destroy = 10
        
    }
}