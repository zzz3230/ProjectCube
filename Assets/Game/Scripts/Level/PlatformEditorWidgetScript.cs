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
    [SerializeField] TriggerArgsFieldParamEditorScript _triggerArgsFieldParamEditorOriginal;


    [SerializeField] PlatformParams _params;
    [SerializeField] BasePlatformScript _platform;

    [SerializeField] UIMouseDetectorScript[] _saveZones;

    List<FieldInfo> _paramsFields = new();
    List<ParamEditorScript> _paramsWidgets = new();

    AngleFieldParamEditorScript _rotationFieldParamEditor;

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
        if (Input.GetKeyDown(KeyCode.R))
        {
            _rotationFieldParamEditor.StartForceInput(Camera.main.WorldToScreenPoint(_platform.transform.position));
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            _rotationFieldParamEditor.StopForceInput();
        }
        if (Input.GetAxis("Delete") == 1f && _platform)
            DestroyPlatform();

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
            //gameObject.SetActive(true);
            SetupParams(_platform.platformParams);
            _platformNameText.text = platform.name;
        }
        //else
        //    gameObject.SetActive(false);
            
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

        _paramsFields.Clear();// = props.ToList();
        _paramsWidgets.Clear();

        for (int i = 0; i < props.Length; i++)
        {
            var attrs = props[i].GetCustomAttributes(true);

            if (attrs.Select(x => x as PlatformParams.HideAttribute).Where(x => x != null).Count() > 0)
                continue;
            

            var displayName = "~name";

            displayName = attrs.Select(
                x => x as PlatformParams.DisplayNameAttribute
                ).Where(
                    x => x != null
                        ).First().name;

            ParamEditorScript spawnedPlatform = null;


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
                floatWidget.range = range;

                spawnedPlatform = floatWidget;
            }
            else if(props[i].FieldType == typeof(Vector2))
            {
                var vector2Widget = Instantiate(_vector2FieldParamWidgetOriginal);

                spawnedPlatform = vector2Widget;
            }
            else if(props[i].FieldType == typeof(Angle))
            {
                var angleWidget = Instantiate(_angleFieldParamEditorOriginal);

                spawnedPlatform = angleWidget;
                _rotationFieldParamEditor = angleWidget;
            }
            else if (props[i].FieldType == typeof(TriggerArgs))
            {
                var triggerWidget = Instantiate(_triggerArgsFieldParamEditorOriginal);

                spawnedPlatform = triggerWidget;
            }

            if (spawnedPlatform)
            {
                spawnedPlatform.transform.SetParent(_paramsFieldsParent);

                spawnedPlatform.Init(this, props[i].Name);
                spawnedPlatform.SetDisplayName(displayName); 
                _paramsWidgets.Add(spawnedPlatform);


                _paramsFields.Add(props[i]);
            }
        }

        var tools = Instantiate(_platformToolsParamWidgetOriginal);
        tools.transform.SetParent(_paramsFieldsParent);
        tools.Init(this);
    }
}
