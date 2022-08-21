using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuSchematicLoaderButtonWidgetScript : MonoBehaviour
{
    string saveName;
    [SerializeField] TMP_Text nameText;
    SchematicManagerScript _schematicManager;

    public void Init(string name, SchematicManagerScript m)
    {
        saveName = name;
        nameText.text = name;
        _schematicManager = m;
    }

    public void Button_LoadClick()
    {
        _schematicManager.Load(saveName);
    }
}
