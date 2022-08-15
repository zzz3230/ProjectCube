using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalReceiverScript : MonoBehaviour
{
    [SerializeField] SignalTransmitterScript _autoconnectTransmitter;

    private void Start()
    {
        if(_autoconnectTransmitter)
            Connect(_autoconnectTransmitter, null);
    }

    public event Action<SignalValue> onSignalChanged;
    
    public bool IsConnected(SignalTransmitterScript transmitter)
    {
        return transmitter == _connectedTransmitter;
    }

    private SignalTransmitterScript _connectedTransmitter;
    private SignalLineScript _signalLine;

    public void Connect(SignalTransmitterScript transmitter, SignalLineScript line)
    {
        _signalLine = line;

        if(_connectedTransmitter != null)
            Disconnect(_connectedTransmitter);

        _connectedTransmitter = transmitter;
        transmitter.onSignalChanged += SignalValueChanged;
    }

    void SignalValueChanged(SignalValue val)
    {
        onSignalChanged?.Invoke(val);
    }

    public void Disconnect(SignalTransmitterScript transmitter)
    {
        if(_signalLine)
            Destroy(_signalLine.gameObject);

        _connectedTransmitter.onSignalChanged -= SignalValueChanged;

        _connectedTransmitter = null;
    }
    private void OnDestroy()
    {
        if(_connectedTransmitter != null)
            Disconnect(_connectedTransmitter);
    }
}
