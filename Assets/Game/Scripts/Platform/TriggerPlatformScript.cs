using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatformParams : PlatformParams
{
    [DisplayName("Distance")]
    [FloatRange(0f, 100f)]
    public float distance = 5;

    //[DisplayName("Direction")]
    //public Vector2 direction = Vector2.right;

    [DisplayName("Args")]
    public TriggerArgs args;

    public override string ToString()
    {
        return base.ToString() + $"dist: {distance}; args: {args};";
    }
}


public class TriggerPlatformScript : MovablePlatformScript, ISignalConnectable
{
    [SerializeField] CubeTriggerScript _triggerScript;
    
    public override void Created()
    {
        platformParams = new TriggerPlatformParams { args = new TriggerArgs { mode = TriggerMode.Switch } };
        base.Created();
    }

    private void Start()
    {
        
    }

    public override void ApplyParams()
    {
        base.ApplyParams();

        if (platformParams is TriggerPlatformParams p)
        {
            //_triggerScript.direction = p.direction;
            _triggerScript.maxDistance = p.distance;
            _triggerScript.triggerMode = p.args.mode;
        }
        else
        {
            throw new Exception("bad params type");
        }
        
    }

    public override void Reset()
    {
    }
}
