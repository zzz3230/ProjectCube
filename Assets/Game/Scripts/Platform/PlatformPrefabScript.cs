using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPrefabScript : MonoBehaviour
{
    [SerializeField] BasePlatformScript _platformScript;
    public BasePlatformScript platformScript => _platformScript;

    [SerializeField] BuildingID _buildingID;
    public BuildingID buildingID => _buildingID;

    public void InitPlatform(PlatformParams customParams)
    {
        platformScript.Created();
        if(customParams != null)
            platformScript.platformParams = customParams;
        platformScript.ApplyParams();
    }
}
