using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlatformScript : ResettableMonoBehaviour
{
    [SerializeField] PlatformPrefabScript _prefab;
    public PlatformPrefabScript prefab => _prefab;

    public PlatformParams platformParams;

    //private void Awake()
    //{
    //    Created();
    //}

    

    public virtual void Created()
    {
        platformParams.uuid = System.Guid.NewGuid().ToString();
    }

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
