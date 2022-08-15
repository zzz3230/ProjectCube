using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueGetterScript : MonoBehaviour
{
    public event Action<float> onFinalValueChanged;

    protected void CallValueChanged(float newVal)
    {
        onFinalValueChanged?.Invoke(newVal);
    }
}
