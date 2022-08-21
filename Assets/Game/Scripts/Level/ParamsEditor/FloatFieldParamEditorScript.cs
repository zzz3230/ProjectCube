// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatFieldParamEditorScript : ParamEditorScript, IValidatable
{
    [SerializeField] TMP_InputField _inputField;
    [SerializeField] TMP_Text _nameText;

    public string displayName { set => _nameText.text = value; }

    public override void SetDisplayName(string name)
    {
        displayName = name;
    }

    //float _value;_value = value;d

    Color _correctColor;
    Color _incorrectColor;
    public FloatRange range;

    private void Start()
    {
        _inputField.onValueChanged.AddListener((s) => { if(GetValue().correct)  SyncValue(); });


        _correctColor = Color.black;// _inputField.textComponent.color;
        //Debug.Log(_correctColor);

        _incorrectColor = Color.red;
    }

    public override void SyncValue()
    {
        var v = GetValue();
        if (v.correct)
        {
            UpdateValue(v.value);
        }
        else
            Debug.LogWarning("input incorrect");
    }

    void SetCorrectInputColor(bool correct)
    {
        _inputField.textComponent.color = correct ? _correctColor : _incorrectColor;
    }

    public float value { get => GetValue().value; set {  ChangeValue(value); } }

    public override void SetValue(object v)
    {
        value = (float)v;
    }
    public override object GetFinalValue()
    {
        return GetValue().value;
    }

    (bool correct, float value) GetValue()
    {
        var textValue = _inputField.text;

        if (textValue == "")
        {
            SetCorrectInputColor(true);
            //_inputField.text = "0";
            return (true, 0f);
        }

        
        if(Utils.TryParseFloat(textValue, out var r))
        {
            SetCorrectInputColor(true);
            return (true, r);
        }
        else
        {
            SetCorrectInputColor(false);
            //Debug.LogWarning("intput is not float");
            return (false, -666f);
        }
    }

    void ChangeValue(float val)
    {
        _inputField.text = val.ToString();
    }

    public ValidateResult Validate()
    {
        return ValidateResult.Ok();
    }
}
