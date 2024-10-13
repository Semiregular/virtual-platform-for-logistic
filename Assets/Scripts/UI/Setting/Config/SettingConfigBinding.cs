using System;
using Config;
using UnityEngine;

namespace UI.Setting.Config
{
    public class SettingConfigBinding : MonoBehaviour
{
    public static SettingConfigBinding Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private static int IsIntValid(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return -1;
        }
        
        try
        {
            var x = int.Parse(s);
            return x;
        }
        catch (FormatException ex)
        {
            Debug.LogError("Invalid int: " + s);
            Debug.LogError(ex);
            return -1;
        }
    }
    
    private static float IsFloatValid(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return -1;
        }
        try
        {
            var x = float.Parse(s);
            return  x;
        }
        catch (FormatException ex)
        {
            Debug.LogError("Invalid float: " + s);
            Debug.LogError(ex);
            return -1;
        }
    }
    

    // ============================================================================
    
    // Map
    public void MapSizeX(string s)
    {
        var res = IsIntValid(s);
        if (res != -1)
        {
            var mapSize = GlobalConfig.mapConfig.MapSize;
            mapSize.x = res;
            GlobalConfig.mapConfig.MapSize = mapSize;
        }
    }

    public void MapSizeZ(string s)
    {
        var res = IsIntValid(s);
        if (res != -1)
        {
            var mapSize = GlobalConfig.mapConfig.MapSize;
            mapSize.z = res;
            GlobalConfig.mapConfig.MapSize = mapSize;
        }
    }

    public void MapFrictionStatic(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            GlobalConfig.mapConfig.MapFrictionStatic = res;
        }
    }

    public void MapFrictionKinetic(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            GlobalConfig.mapConfig.MapFrictionKinetic = res;
        }
    }

    // ============================================================================

    // Aruco
    public void ArucoSpacingX(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var arucoSpacing = GlobalConfig.arucoConfig.ArucoSpacing;
            arucoSpacing.x = res;
            GlobalConfig.arucoConfig.ArucoSpacing = arucoSpacing;
        }
    }

    public void ArucoSpacingZ(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var arucoSpacing = GlobalConfig.arucoConfig.ArucoSpacing;
            arucoSpacing.z = res;
            GlobalConfig.arucoConfig.ArucoSpacing = arucoSpacing;
        }
    }

    // ============================================================================

    // Stack Group
    public void StackGroupLayoutX(string s)
    {
        var res = IsIntValid(s);
        if (res != -1)
        {
            var stackGroupLayout = GlobalConfig.stackGroupConfig.StackGroupLayout;
            stackGroupLayout.x = res;
            GlobalConfig.stackGroupConfig.StackGroupLayout = stackGroupLayout;
        }
    }

    public void StackGroupLayoutZ(string s)
    {
        var res = IsIntValid(s);
        if (res != -1)
        {
            var stackGroupLayout = GlobalConfig.stackGroupConfig.StackGroupLayout;
            stackGroupLayout.z = res;
            GlobalConfig.stackGroupConfig.StackGroupLayout = stackGroupLayout;
        }
    }

    public void StackGroupSpacingX(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var stackGroupSpacing = GlobalConfig.stackGroupConfig.StackGroupSpacing;
            stackGroupSpacing.x = res;
            GlobalConfig.stackGroupConfig.StackGroupSpacing = stackGroupSpacing;
        }
    }

    public void StackGroupSpacingZ(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var stackGroupSpacing = GlobalConfig.stackGroupConfig.StackGroupSpacing;
            stackGroupSpacing.z = res;
            GlobalConfig.stackGroupConfig.StackGroupSpacing = stackGroupSpacing;
        }
    }

    // ============================================================================
    
    // Stack KIVA
    
    public void StackKivaSizeX(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var stackLayerSize = GlobalConfig.stackKiva.StackLayerSize;
            stackLayerSize.x = res;
            GlobalConfig.stackKiva.StackLayerSize = stackLayerSize;
        }
    }

    public void StackKivaSizeY(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var stackLayerSize = GlobalConfig.stackKiva.StackLayerSize;
            stackLayerSize.y = res;
            GlobalConfig.stackKiva.StackLayerSize = stackLayerSize;
        }
    }

    public void StackKivaSizeZ(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var stackLayerSize = GlobalConfig.stackKiva.StackLayerSize;
            stackLayerSize.z = res;
            GlobalConfig.stackKiva.StackLayerSize = stackLayerSize;
        }
    }

    public void StackKivaLayer(string s)
    {
        var res = IsIntValid(s);
        if (res != -1)
        {
            GlobalConfig.stackKiva.StackLayerNum = res;
        }
    }
    
    public void StackKivaBottomHeight(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            GlobalConfig.stackKiva.StackBottomHeight = res;
        }
    }
    
    public void StackKivaSpacingX(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var stackSpacing = GlobalConfig.stackKiva.StackSpacing;
            stackSpacing.x = res;
            GlobalConfig.stackKiva.StackSpacing = stackSpacing;
        }
    }

    public void StackKivaSpacingZ(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var stackSpacing = GlobalConfig.stackKiva.StackSpacing;
            stackSpacing.z = res;
            GlobalConfig.stackKiva.StackSpacing = stackSpacing;
        }
    }

    // ============================================================================
    
    // Stack CTU
    
    public void StackCtuSizeX(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var stackLayerSize = GlobalConfig.stackCtu.StackLayerSize;
            stackLayerSize.x = res;
            GlobalConfig.stackCtu.StackLayerSize = stackLayerSize;
        }
    }

    public void StackCtuSizeY(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var stackLayerSize = GlobalConfig.stackCtu.StackLayerSize;
            stackLayerSize.y = res;
            GlobalConfig.stackCtu.StackLayerSize = stackLayerSize;
        }
    }

    public void StackCtuSizeZ(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var stackLayerSize = GlobalConfig.stackCtu.StackLayerSize;
            stackLayerSize.z = res;
            GlobalConfig.stackCtu.StackLayerSize = stackLayerSize;
        }
    }

    public void StackCtuLayer(string s)
    {
        var res = IsIntValid(s);
        if (res != -1)
        {
            GlobalConfig.stackCtu.StackLayerNum = res;
        }
    }
    
    public void StackCtuBottomHeight(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            GlobalConfig.stackCtu.StackBottomHeight = res;
        }
    }
    
    public void StackCtuSpacingX(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var stackSpacing = GlobalConfig.stackCtu.StackSpacing;
            stackSpacing.x = res;
            GlobalConfig.stackCtu.StackSpacing = stackSpacing;
        }
    }

    public void StackCtuSpacingZ(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var stackSpacing = GlobalConfig.stackCtu.StackSpacing;
            stackSpacing.z = res;
            GlobalConfig.stackCtu.StackSpacing = stackSpacing;
        }
    }

    // ============================================================================
    
    // Box CTU
    
    public void BoxCtuSizeX(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var boxSize = GlobalConfig.boxCtu.BoxSize;
            boxSize.x = res;
            GlobalConfig.boxCtu.BoxSize = boxSize;
        }
    }

    public void BoxCtuSizeY(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var boxSize = GlobalConfig.boxCtu.BoxSize;
            boxSize.y = res;
            GlobalConfig.boxCtu.BoxSize = boxSize;
        }
    }

    public void BoxCtuSizeZ(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var boxSize = GlobalConfig.boxCtu.BoxSize;
            boxSize.z = res;
            GlobalConfig.boxCtu.BoxSize = boxSize;
        }
    }

    public void BoxCtuLayoutX(string s)
    {
        var res = IsIntValid(s);
        if (res != -1)
        {
            var boxLayout = GlobalConfig.boxCtu.BoxLayout;
            boxLayout.x = res;
            GlobalConfig.boxCtu.BoxLayout = boxLayout;
        }
    }

    public void BoxCtuLayoutZ(string s)
    {
        var res = IsIntValid(s);
        if (res != -1)
        {
            var boxLayout = GlobalConfig.boxCtu.BoxLayout;
            boxLayout.z = res;
            GlobalConfig.boxCtu.BoxLayout = boxLayout;
        }
    }

    public void BoxCtuSpacingX(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var boxSpacing = GlobalConfig.boxCtu.BoxSpacing;
            boxSpacing.x = res;
            GlobalConfig.boxCtu.BoxSpacing = boxSpacing;
        }
    }

    public void BoxCtuSpacingZ(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var boxSpacing = GlobalConfig.boxCtu.BoxSpacing;
            boxSpacing.z = res;
            GlobalConfig.boxCtu.BoxSpacing = boxSpacing;
        }
    }

    // ============================================================================

    // Box KIVA
    public void BoxKivaSizeX(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var boxSize = GlobalConfig.boxKiva.BoxSize;
            boxSize.x = res;
            GlobalConfig.boxKiva.BoxSize = boxSize;
        }
    }

    public void BoxKivaSizeY(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var boxSize = GlobalConfig.boxKiva.BoxSize;
            boxSize.y = res;
            GlobalConfig.boxKiva.BoxSize = boxSize;
        }
    }

    public void BoxKivaSizeZ(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var boxSize = GlobalConfig.boxKiva.BoxSize;
            boxSize.z = res;
            GlobalConfig.boxKiva.BoxSize = boxSize;
        }
    }

    public void BoxKivaLayoutX(string s)
    {
        var res = IsIntValid(s);
        if (res != -1)
        {
            var boxLayout = GlobalConfig.boxKiva.BoxLayout;
            boxLayout.x = res;
            GlobalConfig.boxKiva.BoxLayout = boxLayout;
        }
    }

    public void BoxKivaLayoutZ(string s)
    {
        var res = IsIntValid(s);
        if (res != -1)
        {
            var boxLayout = GlobalConfig.boxKiva.BoxLayout;
            boxLayout.z = res;
            GlobalConfig.boxKiva.BoxLayout = boxLayout;
        }
    }

    public void BoxKivaSpacingX(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var boxSpacing = GlobalConfig.boxKiva.BoxSpacing;
            boxSpacing.x = res;
            GlobalConfig.boxKiva.BoxSpacing = boxSpacing;
        }
    }

    public void BoxKivaSpacingZ(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            var boxSpacing = GlobalConfig.boxKiva.BoxSpacing;
            boxSpacing.x = res;
            GlobalConfig.boxKiva.BoxSpacing = boxSpacing;
        }
    }
    
    // ============================================================================
    
    // Obj
    
    public void ObjCategory(string s)
    {
        var res = IsIntValid(s);
        if (res != -1)
        {
            GlobalConfig.objConfig.ObjCategory = res;
        }
    }
    
    public void ObjType(string s)
    {
        var res = IsIntValid(s);
        if (res != -1)
        {
            GlobalConfig.objConfig.ObjType = res;
        }
    }
    
    // ============================================================================
    
    // Task

    public void TaskNum(string s)
    {
        var res = IsIntValid(s);
        if (res != -1)
        {
            GlobalConfig.taskConfig.TaskNum = res;
        }
    }
    
    public void TaskObjSize(string s)
    {
        var res = IsIntValid(s);
        if (res != -1)
        {
            GlobalConfig.taskConfig.TaskObjSize = res;
        }
    }
    
    public void TaskIncSecond(string s)
    {
        var res = IsFloatValid(s);
        if (Math.Abs(res + 1) < 0.01f)
        {
            GlobalConfig.taskConfig.TaskIncSecond = res;
        }
    }
    
    public void TaskAutoInc(bool b)
    {
        GlobalConfig.isTaskAutoInc = !GlobalConfig.isTaskAutoInc;
    }
    
    public void TaskAutoAssign(bool b)
    {
        GlobalConfig.isTaskAutoAssign = !GlobalConfig.isTaskAutoAssign;
    }
    
    
    // ============================================================================
    
    // Socket

    public void SendWebSocketUrl(string s)
    {
        GlobalConfig.positionSendWebSocketUrl = s;
    }

    public void SendInterval(string s)
    {
        var res = IsIntValid(s);
        if (res != -1)
        {
            GlobalConfig.positionSendInterval = res;
        }
    }
    
    public void RevWebSocketUrl(string s)
    {
        GlobalConfig.commandRevWebSocketUrl = s;
    }

    public void RevInterval(string s)
    {
        var res = IsIntValid(s);
        if (res != -1)
        {
            GlobalConfig.commandRevInterval = res;
        }
    }
    
    public  void EnableRcsControl(bool b)
    {
        GlobalConfig.enableRcsControl = !GlobalConfig.enableRcsControl;
    }
    
    // ============================================================================
}

}
