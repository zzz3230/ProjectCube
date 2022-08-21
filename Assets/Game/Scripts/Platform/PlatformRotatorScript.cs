using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
using UnityEngine.EventSystems;

public class PlatformRotatorParams : PlatformParams
{
    [DisplayName("Absent Angle")]
    [FloatRange(-90f, 90f)]
    public float absentAngle = 0;

    [DisplayName("Present Angle")]
    [FloatRange(-90f, 90f)]
    public float presentAngle = -45;

    public override string ToString()
    {
        return base.ToString() + $"abs: {absentAngle}; pre: {presentAngle}; ";
    }
}


public class PlatformRotatorScript : BasePlatformScript, ISignalConnectable
{
    #region debug

    [Button]
    void Debug_Move0()
    {
        ChangeAngle(0);
    }
    [Button]
    void Debug_Move30()
    {
        ChangeAngle(30);
    }
    [Button]
    void Debug_Move45()
    {
        ChangeAngle(45);
    }
    [Button]
    void Debug_Move90()
    {
        ChangeAngle(90);
    }
    [Button(name: "Debug_Move -30")]
    void Debug_MoveN30()
    {
        ChangeAngle(-30);
    }
    [Button(name: "Debug_Move -45")]
    void Debug_MoveN45()
    {
        ChangeAngle(-45);
    }
    #endregion

    public override void Created()
    {
        platformParams = new PlatformRotatorParams(); 
        base.Created();
    }

    public override void ApplyParams()
    {
        base.ApplyParams();
        
        if (platformParams is PlatformRotatorParams p)
        {
            if (autoConnectingGetter is SignalConverterScript conv)
            {
                conv.absentValue = p.absentAngle; 
                SetAbsentRotation();
                conv.presentValue = p.presentAngle;
            }
            else
            {
                throw new Exception("getter is not converter");
            }
        }
        else
        {
            throw new Exception("bad params type");
        }
    }


    [SerializeField] private ValueGetterScript autoConnectingGetter;
    [SerializeField] private PlatformScript platformScript;
    [SerializeField] private float platformRepulseMp = 160;
    private void Start()
    {
        if(autoConnectingGetter)
            autoConnectingGetter.onFinalValueChanged += delegate(float f)
            {
                ChangeAngle(f);
            }; 
    }

    void ChangeAngle(float newAngle)
    {
        _lerpT = 0;
        _startRotation = transform.rotation;
        var eulerAngle = _startRotation.eulerAngles;
        eulerAngle.z = newAngle;
        _destinationRotation.eulerAngles = eulerAngle;
        platformScript.moving = true;
        platformScript.speed = speed * platformRepulseMp;
    }

    [SerializeField] private float speed = 1;
    
    private Quaternion _startRotation;
    private Quaternion _destinationRotation;
    private float _lerpT = 2f;
    void Update()
    {
        if (_lerpT < 2f)
        {
            transform.rotation = Quaternion.Lerp(_startRotation, _destinationRotation, _lerpT);
            _lerpT += Time.deltaTime * (speed);
            // / Quaternion.Angle(_startRotation, _destinationRotation)

            if (transform.rotation == _destinationRotation)
            {
                _lerpT = 2f;
                platformScript.moving = false;
            }
                
        }
    }

    void SetAbsentRotation()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, (autoConnectingGetter as SignalConverterScript).absentValue);
    }

    public override void Reset()
    {
        //0f
        SetAbsentRotation();
        _lerpT = 2f;
        platformScript.moving = false;
    }


}
