using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatformScript : BasePlatformScript
{
    [SerializeField] private GameObject root;
    public override void ApplyParams()
    {
        root.transform.position = platformParams.position;
        root.transform.rotation = Quaternion.Euler(root.transform.rotation.eulerAngles.WithZ(platformParams.rotation.angle));
    }
}
