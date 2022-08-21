using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using EasyButtons;
using Newtonsoft.Json;
using System.IO;

public class SchematicManagerScript : MonoBehaviour
{
    [SerializeField] LevelBuilderScript _levelBuilder;
    [SerializeField] GameobjectSelectingControllerScript _selectingController;

    string savingFolder => Utils.GetDataFolderPath("schematic");

    [Button]
    void Debug_PrintSchematic()
    {
        var sc = GetSchematicInfo();
        for (int i = 0; i < sc.elements.Count; i++)
        {
            Debug.Log(i + ": " + sc.elements[i].ToString());
        }
    }

    [Button]
    void Debug_SaveSchematic()
    {
        //Debug.Log(Application.dataPath);
        //Debug.Log(Utils.GetDataFolderPath("schematic"));
        SaveSchematic(GetSchematicInfo());
    }
    [SerializeField] string toLoadName;
    [Button]
    void Debug_LoadSchematic()
    {
        ApplySchematic(LoadSchematic(toLoadName));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Save(string name)
    {
        var sc = GetSchematicInfo();
        sc.name = name;

        SaveSchematic(sc);
    }
    public void Load(string name)
    {
        var sc = LoadSchematic(name);
        ApplySchematic(sc);
    }

    public List<string> AvailableSchematics()
    {
        if(!Directory.Exists(savingFolder))
            Directory.CreateDirectory(savingFolder);

        return (
            from d in Directory.GetFiles(savingFolder)
               where d.EndsWith(".pcsc.json") select new FileInfo(d).Name
               ).ToList();
    }

    public void ApplySchematic(SchematicInfo sc)
    {
        Dictionary<string, PlatformPrefabScript> spawned = new();

        for (int i = 0; i < sc.elements.Count; i++)
        {
            var el = sc.elements[i];
            spawned.Add(el.platformParams.uuid, _levelBuilder.Build(el.buildingID, Vector3.zero, el.platformParams));
        }

        for (int i = 0; i < sc.elements.Count; i++)
        {
            var el = sc.elements[i];
            if (!string.IsNullOrEmpty(el.connectedWith))
            {
                _selectingController.ConnectObjects(
                    spawned[el.connectedWith].platformScript.gameObject, 
                    spawned[el.platformParams.uuid].platformScript.gameObject
                    );
            }
        }
    }

    public SchematicInfo LoadSchematic(string name)
    {
        var settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Objects
        };
        var content = File.ReadAllText(Utils.GetDataFolderPath("schematic") + name);
        return (SchematicInfo)JsonConvert.DeserializeObject(content, typeof(SchematicInfo), settings);
    }

    public void SaveSchematic(SchematicInfo sc)
    {
        var settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Objects
        };

        var content = JsonConvert.SerializeObject(sc, settings);
        
        if (!Directory.Exists(Utils.GetDataFolderPath("schematic")))
            Directory.CreateDirectory(Utils.GetDataFolderPath("schematic"));

        File.WriteAllText(Utils.GetDataFolderPath("schematic") + sc.name + ".pcsc.json", content);
    }


    public SchematicInfo GetSchematicInfo()
    {
        var platforms = GameObject.FindObjectsOfType<PlatformPrefabScript>();
        //.Select(x => x.platformScript.platformParams)d

        var platformsParams = platforms.Select(
            x => new SchematicElement { 
                buildingID = x.buildingID, 
                platformParams = x.platformScript.platformParams,
                connectedWith = 
                    x.platformScript is ISignalConnectable ? 
                        (x.platformScript.TryGetComponent<SignalReceiverScript>(out var a) ? 
                            (a.transmitter.TryGetComponent<BasePlatformScript>(out var pl) ?
                                pl.platformParams.uuid 
                                : null) 
                             : null ) 
                         : null
            }
            );

        return new SchematicInfo { name = "save_" + System.DateTime.Now.Ticks, elements = platformsParams.ToList() };
    }
}
