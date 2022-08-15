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
    public GameObject prefab;
}

public class LevelBuilderScript : MonoBehaviour
{
    [SerializeField] List<BuildingInfo> buildings = new();

    public void Build(BuildingID id, Vector3 pos)
    {
        var building = Instantiate(buildings.Find(x => x.id == id).prefab);
        building.transform.position = pos;
    }
}
