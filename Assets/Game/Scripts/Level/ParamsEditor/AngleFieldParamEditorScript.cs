using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleFieldParamEditorScript : ParamEditorScript
{
    [SerializeField] AngleFieldInputRotatorScript _angleInput;

    public override object GetFinalValue()
    {
        return new Angle { angle = _angleInput.angle };
    }

    public override void SetValue(object v)
    {
        //Debug.Log(gameObject);
        _angleInput.SetAngle(((Angle)v).angle);
    }

    void Start()
    {
        _angleInput.angleChanged += (v) => SyncValue();
    }

    public override void SyncValue()
    {
        UpdateValue(GetFinalValue());
    }
}
