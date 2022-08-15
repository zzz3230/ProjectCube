using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SignalUtils
{
    public static SignalValue Inverse(this SignalValue val)
    {
        return val == SignalValue.Present ? SignalValue.Absent : SignalValue.Present;
    }
}
public enum SignalValue
{
    Absent, Present, 
}
public class SignalTransmitterScript : MonoBehaviour
{
    public event Action<SignalValue> onSignalChanged;

    public void ChangeSignalValue(SignalValue newVal)
    {
        onSignalChanged?.Invoke(newVal);
    }
}
