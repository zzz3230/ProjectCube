using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRectangleParams : PlatformParams
{
    [DisplayName("Width")]
    [FloatRange(.1f, 10f)]
    public float width = 3;

    [DisplayName("Height")]
    [FloatRange(.1f, 10f)]
    public float height = 1;

    public override string ToString()
    {
        return base.ToString() + $"wid: {width}; hei: {height}; ";
    }
}

public class PlatformRectangleScript : MovablePlatformScript
{
    public override void Created()
    {
        platformParams = new PlatformRectangleParams { height = transform.localScale.y, width = transform.localScale.x };
        base.Created();
    }

    public override void ApplyParams()
    {
        base.ApplyParams();
        if (platformParams is PlatformRectangleParams p)
        {
            transform.localScale = new Vector3(p.width, p.height, 1);

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
