using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalConverterScript : ValueGetterScript
{
    [SerializeField] private SignalReceiverScript autoConnectingReceiver;
    
    [SerializeField] public float presentValue = -30;
    [SerializeField] public float absentValue = 0;
    
    
    private void Start()
    {
        if(autoConnectingReceiver != null)
            Connect(autoConnectingReceiver);
    }

    public void Connect(SignalReceiverScript receiver)
    {
        receiver.onSignalChanged += SignalValueChanged;
    }

    void SignalValueChanged(SignalValue val)
    {
        CallValueChanged(val == SignalValue.Present ? presentValue : absentValue);
    }

    private void OnDestroy()
    {
        //autoConnectingReceiver.Disconnect()
    }

}
