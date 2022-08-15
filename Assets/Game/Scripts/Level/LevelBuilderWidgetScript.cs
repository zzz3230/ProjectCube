using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilderWidgetScript : MonoBehaviour
{
    public LevelBuilderScript levelBuilderScript;

    public void BuildByID(BuildingID id)
    {
        levelBuilderScript.Build(id, Vector3.one);
    }

    public void Build_MoveHorizontalPlatform()
    {
        BuildByID(BuildingID.MoveHorizontalPlatform);
    }
    public void Build_MoveVerticalPlatform()
    {
        BuildByID(BuildingID.MoveVerticalPlatform);
    }
    public void Build_RepelPlatform()
    {
        BuildByID(BuildingID.RepelPlatform);
    }

    public void Build_RepulsiveRect()
    {
        BuildByID(BuildingID.RepulsiveRect);
    }

    public void Build_AcceleratingRect()
    {
        BuildByID(BuildingID.AcceleratingRect);
    }
    public void Build_WallRect()
    {
        BuildByID(BuildingID.WallRect);
    }

    public void Build_Trigger()
    {
        BuildByID(BuildingID.Trigger);
    }
}
