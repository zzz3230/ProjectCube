using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerArgsFieldParamEditorScript : ParamEditorScript
{
    [SerializeField] Toggle _buttonToggle;
    [SerializeField] Toggle _switchToggle;

    private void Start()
    {
        _buttonToggle.onValueChanged.AddListener((v) => SyncValue());
        _switchToggle.onValueChanged.AddListener((v) => SyncValue());
    }

    TriggerArgs value { 
        get
        {
            TriggerArgs res = new TriggerArgs();

            if (_switchToggle.isOn)
                res.mode |= TriggerMode.Switch;
            if(_buttonToggle.isOn)
                res.mode |= TriggerMode.Button;

            return res;
        }
        set 
        {
            _switchToggle.isOn = value.mode.HasFlag(TriggerMode.Switch);
            _buttonToggle.isOn = value.mode.HasFlag(TriggerMode.Button);
        }
    }
    public override void SyncValue()
    {
        UpdateValue(value);
    }

    public override void SetValue(object v)
    {
        value = (TriggerArgs)v;
    }

    public override object GetFinalValue()
    {
        return value;
    }
    //null
}
