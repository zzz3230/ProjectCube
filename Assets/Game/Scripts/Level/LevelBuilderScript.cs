using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuildingID
{
    RepelPlatform,
    MoveHorizontalPlatform,
    MoveVerticalPlatform,
    Trigger,
    WallRect,
    RepulsiveRect,
    AcceleratingRect
}

[System.Serializable]
public struct BuildingInfo
{
    public BuildingID id;
    public PlatformPrefabScript prefab;
    //public BasePlatformScript platformScript;
}

public class LevelBuilderScript : MonoBehaviour
{
    [SerializeField] List<BuildingInfo> buildings = new();

    public PlatformPrefabScript Build(BuildingID id, Vector3 pos, PlatformParams customParams = null, bool forceSetPosInParams = false)
    {
        if (!LevelScript.instance.canEditLevel)
            return null;

        var info = buildings.Find(x => x.id == id);

        var building = Instantiate(info.prefab);
        //print("new pos " + pos);

        if(forceSetPosInParams && customParams != null)
            customParams.position = pos;

        building.transform.position = pos;
        building.InitPlatform(customParams);
        //building.GetComponentOrInChildren<BasePlatformScript>().Created();
        //info.platformScript.vnull 
        return building;
    }
    public PlatformPrefabScript BuildInCenter(BuildingID id, PlatformParams customParams = null)
    {
        return Build(id, Camera.main.ViewportToWorldPoint(new Vector2(.5f, .5f)).WithZ(0).Round(.5f), customParams, true);
    }
}
