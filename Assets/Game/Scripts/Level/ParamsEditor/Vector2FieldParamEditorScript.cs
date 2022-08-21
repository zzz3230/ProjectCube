using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Vector2FieldParamEditorScript : ParamEditorScript, IValidatable
{
    [SerializeField] TMP_InputField _xInputField;
    [SerializeField] TMP_InputField _yInputField;

    [SerializeField] TMP_Text _nameText;

    public string displayName { set => _nameText.text = value; }

    public override void SetDisplayName(string name)
    {
        displayName = name;
    }

    Color _correctColor;
    Color _incorrectColor;

    void Start()
    {
        _xInputField.onValueChanged.AddListener((s) => { if (GetXValue().correct) SyncValue(); });
        _yInputField.onValueChanged.AddListener((s) => { if (GetYValue().correct) SyncValue(); });

        _correctColor = Color.black;// _xInputField.textComponent.color;
        _incorrectColor = Color.red;
    }

    public override void SyncValue()
    {
        var x = GetXValue();
        var y = GetYValue();
        if (x.correct && y.correct)
        {
            UpdateValue(new Vector2(x.value, y.value));
        }
        else
            Debug.LogWarning("input incorrect");
    }

    void SetCorrectInputColor(bool isX, bool correct)
    {
        if(isX)
            _xInputField.textComponent.color = correct ? _correctColor : _incorrectColor;
        else
            _yInputField.textComponent.color = correct ? _correctColor : _incorrectColor;
    }


    public Vector2 value { get => new Vector2(GetXValue().value, GetYValue().value); set { ChangeValue(value); } }


    public override void SetValue(object v)
    {
        //try
        //{
            value = (Vector2)v;
        //}
        //catch
        //{
        //    Debug.Log(v);
        //}
    }
    public override object GetFinalValue()
    {
        return value;
    }

    private void ChangeValue(Vector2 value)
    {
        //throw new System.NotImplementedException();
        _xInputField.text = value.x.ToString();
        _yInputField.text = value.y.ToString();
    }

    (bool correct, float value) GetXValue()
    {
        if (_xInputField.text == "")
        {
            SetCorrectInputColor(true, true);
            return (true, 0f);
        }


        if (float.TryParse(_xInputField.text, out var r))
        {
            SetCorrectInputColor(true, true);
            return (true, r);
        }
        else
        {
            SetCorrectInputColor(true, false);
            return (false, -666f);
        }
    }

    (bool correct, float value) GetYValue()
    {
        if (_yInputField.text == "")
        {
            SetCorrectInputColor(false, true);
            return (true, 0f);
        }


        if (float.TryParse(_yInputField.text, out var r))
        {
            SetCorrectInputColor(false, true);
            return (true, r);
        }
        else
        {
            SetCorrectInputColor(false, false);
            return (false, -666f);
        }
    }

    public ValidateResult Validate()
    {
        return ValidateResult.Ok();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
