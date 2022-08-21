using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchematicElement
{
    public BuildingID buildingID;
    public PlatformParams platformParams;
    public string connectedWith;

    public override string ToString()
    {
        return $"id: {buildingID}; params: ({platformParams})";
    }
}

public class SchematicInfo
{
    public string name;
    public List<SchematicElement> elements;
    

    //public string Serialize()
    //{

    //}
}
