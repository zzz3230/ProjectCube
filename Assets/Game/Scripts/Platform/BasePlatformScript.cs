using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlatformScript : ResettableMonoBehaviour
{
    public PlatformParams platformParams;

    public virtual void ApplyParams()
    {
        transform.position = platformParams.position;
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }

    public virtual void DestroyPlatform()
    {
        Destroy(gameObject);
    }
}
