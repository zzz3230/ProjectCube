using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatformParams : PlatformParams
{
    [DisplayName("Distance")]
    [FloatRange(0f, 100f)]
    public float distance = 5;

    [DisplayName("Direction")]
    public Vector2 direction = Vector2.right;
}


public class TriggerPlatformScript : MovablePlatformScript
{
    [SerializeField] CubeTriggerScript _triggerScript;
    
    private void Awake()
    {
        platformParams = new TriggerPlatformParams();
    }

    private void Start()
    {
        
    }

    public override void ApplyParams()
    {
        base.ApplyParams();

        if (platformParams is TriggerPlatformParams p)
        {
            _triggerScript.direction = p.direction;
            _triggerScript.maxDistance = p.distance;
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
