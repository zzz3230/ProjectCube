using System;
using System.Collections;
using System.Collections.Generic;
//using EasyButtons.Editor.Utils;
using UnityEngine;

[Flags]
public enum TriggerMode
{
    Button = 1,
    Switch = 2,
    OnceOn = 4
}

[RequireComponent(typeof(LineRenderer))]
public class CubeTriggerScript : ResettableMonoBehaviour
{
    [SerializeField] private SignalTransmitterScript autoConnectingTransmitter;
    
    private LineRenderer _lineRenderer;
    //[SerializeField] private 
    public    float maxDistance;
    //[SerializeField] private 
    public    Vector2 direction = Vector2.right;

    //[SerializeField] List<Collider2D> ignoreColliders = new List<Collider2D>();private
    [SerializeField] LayerMask ignoreTriggerLayerMask;

    private SignalTransmitterScript _connectedTransmitter;

    [SerializeField] public TriggerMode triggerMode;
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 2;
        _connectedTransmitter = autoConnectingTransmitter;
    }

    private SignalValue _lastValue;
    private int _signalChangedCount;
    void ChangeSignalValue(SignalValue newValue)
    {
        if(_signalChangedCount > 0 && triggerMode.HasFlag(TriggerMode.OnceOn))
            return;
        
        if (_lastValue != newValue)
        {
            _connectedTransmitter.ChangeSignalValue(newValue);
            _lastValue = newValue;
            _signalChangedCount++;
        }
    }

    private bool _isFirstHitCall;
    void Update()
    {
        var position = transform.position;

        direction = transform.right;
        
        RaycastHit2D hit = Physics2D.Raycast(position, direction, maxDistance, ~ignoreTriggerLayerMask);
        
        Vector3 end = (direction * maxDistance) + (Vector2)position;
        bool hitted = false;
        
        if (hit.collider)
        {
            //Debug.Log(hit.distance);
            if (hit.distance < maxDistance)
            {
                end = hit.point;
                hitted = true;
            }
        }
        
        _lineRenderer.SetPosition(0, position);
        _lineRenderer.SetPosition(1, end);

        if (hitted)
        {
            if(triggerMode.HasFlag(TriggerMode.Button))
                ChangeSignalValue(SignalValue.Present);
            else if(triggerMode.HasFlag(TriggerMode.Switch))
            {
                if(_isFirstHitCall)
                    ChangeSignalValue(_lastValue.Inverse());
            }

            _isFirstHitCall = false;
        }
        else
        {
            if(triggerMode.HasFlag(TriggerMode.Button))
                ChangeSignalValue(SignalValue.Absent);
            _isFirstHitCall = true;
        }
    }

    public override void Reset()
    {
        if (_lastValue == SignalValue.Present)
        {
            _connectedTransmitter.ChangeSignalValue(SignalValue.Absent);
        }

        _lastValue = SignalValue.Absent;

        _signalChangedCount = 0;

        _isFirstHitCall = false;
    }
}
