using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamEditorScript : MonoBehaviour
{
    string _fieldName;
    PlatformEditorWidgetScript _platformEditor;

    public virtual void SetDisplayName(string name)
    {

    }

    //public object value;


    public virtual void SetValue(object v)
    {

    }

    public virtual object GetFinalValue()
    {
        return null;
    }


    public void Init(PlatformEditorWidgetScript platformEditor, string fieldName)
    {
        _fieldName = fieldName;
        _platformEditor = platformEditor;
    }

    //protected
        public void UpdateValue(object newValue)
    {
        _platformEditor.SetValue(_fieldName, newValue);
    }

    public virtual void SyncValue()
    {

    }
}
