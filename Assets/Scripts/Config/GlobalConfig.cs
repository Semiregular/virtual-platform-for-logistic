using Entity.Stacks;
using UnityEngine;

namespace Config
{
    public static class GlobalConfig {
        
    // ============================================================================
    
    // Map
    
    public static MapConfig mapConfig = new(
        new Vector3(50, 0, 20),
        0.8f,
        0.6f);

    // ============================================================================
    
    // Aruco
    
    public static  ArucoConfig arucoConfig = new(
        new Vector3(0.5f, 0.5f, 0.5f),
        new Vector3(1f, 0, 1f));
    
    // ============================================================================

    // StackGroup
    
    public static StackGroupConfig stackGroupConfig = new(
        new Vector3Int(4, 0, 2),
        new Vector3(2, 0, 2));

    // ============================================================================
    
    // Tiny Map
    
    public static TinyMapConfig tinyMapConfig = new(
        30f,
        10f);

    // ============================================================================
    
    // Stack

    public static StackConfig stackKiva = new(
        new Vector3(1.8f, 0.5f, 0.8f),
        new Vector3(0.2f, 0f, 0.2f),
        3,
        0.3f,
        0.1f,
        0.1f,
        StackType.Kiva);
    
    public static StackConfig stackCtu = new(
        new Vector3(2f, 0.6f, 1f),
        new Vector3(0f, 0f, 0f),
        4,
        2.6f,
        0.1f,
        0.1f,
        StackType.Ctu);

    // ============================================================================
    
    // Box
    
    public static BoxConfig boxKiva = new(
        new Vector3(0.5f, 0.3f, 0.4f),
        new Vector3(0.05f, 0f, 0.05f),
        new Vector3Int(3, 0, 2));

    public static BoxConfig boxCtu = new(
        new Vector3(0.5f, 0.3f, 0.4f),
        new Vector3(0.1f, 0f, 0.1f),
        new Vector3Int(3, 0, 2));

    // ============================================================================

    // Lift
    
    public static int liftNum = 100;

    public static LiftConfig liftCtu = new(
        10,
        5);
    
    public static LiftConfig liftKiva = new(
        1,
        2);

    // ============================================================================

    // Obj
    
    public static  ObjConfig objConfig = new(
        10,
        20);

    // ============================================================================

    // Task
    
    public static TaskConfig taskConfig = new(
        50,
        5,
        4,
        5);
    
    public static bool isTaskAutoInc = true;
    public static bool isTaskAutoAssign = true;

    // ============================================================================
    
    // Socket
    
    public static string positionSendWebSocketUrl = "ws://127.0.0.1:8080/send/position/";
    public static int positionSendInterval = 1000;
    public static string commandRevWebSocketUrl = "ws://127.0.0.1:8080/rev/command/";
    public static int commandRevInterval = 0;
    public static string assignRevWebSocketUrl = "ws://127.0.0.1:8080/rev/assign/";
    public static string mapHttpUrl = "http://127.0.0.1:8088/map/";
    
    public static bool enableRcsControl = true;
    
    // ============================================================================
    }
}
