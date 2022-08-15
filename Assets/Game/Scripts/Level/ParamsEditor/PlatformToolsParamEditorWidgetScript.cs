using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformToolsParamEditorWidgetScript : MonoBehaviour
{
    PlatformEditorWidgetScript _platformEditor;

    public void Button_Destroy()
    {
        _platformEditor.DestroyPlatform();
    }

    internal void Init(PlatformEditorWidgetScript platformEditorWidgetScript)
    {
        _platformEditor = platformEditorWidgetScript;
    }
}
