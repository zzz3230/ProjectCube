using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AngleFieldParamEditorScript : ParamEditorScript
{
    [SerializeField] AngleFieldInputRotatorScript _angleInput;
    [SerializeField] TMP_Text _nameText;

    public override void SetDisplayName(string name)
    {
        _nameText.text = name;
    }

    public void StartForceInput(Vector2 aroud)
    {
        _angleInput.forceInputAround = aroud;
        _angleInput.forceInput = true;
    }
    public void StopForceInput()
    {
        _angleInput.forceInput = false;
    }

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
