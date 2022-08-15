using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SignalLineScript : MonoBehaviour
{
    SignalTransmitterScript _transmtter; 
    SignalReceiverScript _receiver;
    LineRenderer _lineRenderer;

    Vector3 _start;
    Vector3 _end;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.SetPosition(0, Vector3.back);
        _lineRenderer.SetPosition(1, Vector3.back);
    }

    public void Init(SignalTransmitterScript t, SignalReceiverScript r)
    {
        _transmtter = t;
        _receiver = r;
    }

    void Update()
    {
        var newStart = _transmtter.transform.position;
        var newEnd = _receiver.transform.position;

        if(newStart != _start)
        {
            _start = newStart;
            _lineRenderer.SetPosition(0, _start);
        }
        if(newEnd != _end)
        {
            _end = newEnd;
            _lineRenderer.SetPosition(1, _end);
        }

    }
}
