using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TriggerArgs
{
    public TriggerMode mode;

    public override string ToString()
    {
        return $"(mode={mode})";
    }
}
