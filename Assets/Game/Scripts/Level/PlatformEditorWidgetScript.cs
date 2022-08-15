using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Reflection;

public class PlatformEditorWidgetScript : MonoBehaviour
{
    [SerializeField] TMP_Text _platformNameText;
    [SerializeField] RectTransform _paramsFieldsParent;

    [Header("fiels originals")]
    [SerializeField] FloatFieldParamEditorScript _floatParamWidgetOriginal;
    [SerializeField] Vector2FieldParamEditorScript _vector2FieldParamWidgetOriginal;
    [SerializeField] AngleFieldParamEditorScript _angleFieldParamEditorOriginal;
    [SerializeField] PlatformToolsParamEditorWidgetScript _platformToolsParamWidgetOriginal;

    [SerializeField] PlatformParams _params;
    [SerializeField] BasePlatformScript _platform;

    [SerializeField] UIMouseDetectorScript[] _saveZones;

    List<FieldInfo> _paramsFields = new();
    List<ParamEditorScript> _paramsWidgets = new();

    public bool IsMouseInSaveZone()
    {
        return _saveZones.Any(x => x.isMouseIn);
    }

    private void Start()
    {
        //SetupParams(new PlatformRotatorParams());
        if(_platform)
            SetupPlatform(_platform);
    }

    private void Update()
    {
        Debug.Assert(_paramsFields.Count == _paramsWidgets.Count);

        for (int i = 0; i < _paramsFields.Count; i++)
        {
            //var a = _paramsWidgets[i];
            //var val = a.GetFinalValue();

            if (!_paramsWidgets[i].GetFinalValue().Equals(_paramsFields[i].GetValue(_params)))
            {
                _paramsWidgets[i].SetValue(_paramsFields[i].GetValue(_params));
                //Debug.Log("Value updated for " + _paramsFields[i].GetValue(_params));
            }
                
        }
    }

    public void DestroyPlatform()
    {
        _platform.DestroyPlatform();
        SetupPlatform(null);
    }

    public void SetValue(string fieldName, object value)
    {
        _params.GetType().GetField(fieldName).SetValue(_params, value);
        _platform.ApplyParams();
    }

    public void SetupPlatform(BasePlatformScript platform)
    {
        ClearParamsWidgets();

        _paramsFields.Clear();
        _paramsWidgets.Clear();

        _platform = platform;

        if (platform)
        {
            SetupParams(_platform.platformParams);
            _platformNameText.text = platform.name;
        }
            
    }

    void ClearParamsWidgets()
    {
        for (int i = 0; i < _paramsFieldsParent.childCount; i++)
        {
            Destroy(_paramsFieldsParent.GetChild(i).gameObject);
        }
    }

    void SetupParams(PlatformParams p)
    {

        _params = p;

        var props = p.GetType().GetFields();

        _paramsFields = props.ToList();
        _paramsWidgets.Clear();

        for (int i = 0; i < props.Length; i++)
        {
            var attrs = props[i].GetCustomAttributes(true);

            var displayName = "~name";

            displayName = attrs.Select(
                x => x as PlatformParams.DisplayNameAttribute
                ).Where(
                    x => x != null
                        ).First().name;



            if (props[i].FieldType == typeof(System.Single))
            {
                FloatRange range = default;

                for (int j = 0; j < attrs.Length; j++)
                {
                    if(attrs[j] is PlatformParams.FloatRangeAttribute r)
                    {
                        range = r.range;
                    }
                }

                var floatWidget = Instantiate(_floatParamWidgetOriginal);
                floatWidget.transform.SetParent(_paramsFieldsParent);

                floatWidget.Init(this, props[i].Name);
                floatWidget.displayName = displayName;
                floatWidget.range = range;

                _paramsWidgets.Add(floatWidget);   
            }
            else if(props[i].FieldType == typeof(Vector2))
            {
                var vector2Widget = Instantiate(_vector2FieldParamWidgetOriginal);
                vector2Widget.transform.SetParent(_paramsFieldsParent);

                vector2Widget.Init(this, props[i].Name);
                vector2Widget.displayName = displayName;

                _paramsWidgets.Add(vector2Widget);
            }
            else if(props[i].FieldType == typeof(Angle))
            {
                var angleWidget = Instantiate(_angleFieldParamEditorOriginal);
                angleWidget.transform.SetParent(_paramsFieldsParent);

                angleWidget.Init(this, props[i].Name);

                _paramsWidgets.Add(angleWidget);
            }
        }

        var tools = Instantiate(_platformToolsParamWidgetOriginal);
        tools.transform.SetParent(_paramsFieldsParent);
        tools.Init(this);
    }
}
