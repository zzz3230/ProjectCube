using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class PlatformMoverParams : PlatformParams
{
    [DisplayName("Absent Pos")]
    [FloatRange(0f, 0f)]
    public float absentPos = 0;

    [DisplayName("Present Pos")]
    [FloatRange(-10f, 10f)]
    public float presentPos = 5;

    [DisplayName("Speed")]
    [FloatRange(.1f, 10f)]
    public float speed = 1;

    public override string ToString()
    {
        return base.ToString() + $"abs: {absentPos}; pre: {presentPos}; speed: {speed}; ";
    }
}

public enum PlatformMoveMode
{
    Horizontal,
    Vertical
}
public class PlatformMoverScript : MovablePlatformScript, ISignalConnectable
{
    #region debug
    [Button]
    void Debug_MoveUp()
    {
        ChangePos(new Vector2(0, 1));
    }
    [Button]
    void Debug_MoveZero()
    {
        ChangePos(Vector2.zero);
    }
    [Button]
    void Debug_MoveOne()
    {
        ChangePos(new Vector2(
            moveMode == PlatformMoveMode.Horizontal ? 1 : 0, 
            moveMode == PlatformMoveMode.Vertical ? 1 : 0
        ));
    }
    [Button]
    void Debug_MoveTwo()
    {
        ChangePos(new Vector2(
            moveMode == PlatformMoveMode.Horizontal ? 2 : 0, 
            moveMode == PlatformMoveMode.Vertical ? 2 : 0
        ));
    }[Button]
    void Debug_MoveNOne()
    {
        ChangePos(new Vector2(
            moveMode == PlatformMoveMode.Horizontal ? -1 : 0, 
            moveMode == PlatformMoveMode.Vertical ? -1 : 0
        ));
    }
    #endregion

    public override void Created()
    {
        platformParams = new PlatformMoverParams(); 
        base.Created();
    }
    
    /*public override void DestroyPlatform()
    {
        
    }*/

    public override void ApplyParams()
    {
        base.ApplyParams();
        
        if (platformParams is PlatformMoverParams p)
        {
            if (autoConnectingGetter is SignalConverterScript conv)
            {
                conv.absentValue = p.absentPos;
                conv.presentValue = p.presentPos;
            }
            else
            {
                throw new Exception("getter is not converter");
            }
        }
        else
        {
            throw new Exception("bad params type");
        }
    }
    
    
    [SerializeField] private ValueGetterScript autoConnectingGetter;
    [SerializeField] private PlatformScript platformScript;
    [SerializeField] private float platformRepulseMp = 160;
    [SerializeField] private PlatformMoveMode moveMode;
    
    private void Start()
    {
        if(autoConnectingGetter)
            autoConnectingGetter.onFinalValueChanged += delegate(float f)
            {
                ChangePos(new Vector2(
                    moveMode == PlatformMoveMode.Horizontal ? f : 0, 
                    moveMode == PlatformMoveMode.Vertical ? f : 0
                    ));
            }; 
    }

    void ChangePos(Vector2 newPos)
    {
        _lerpT = 0;
        
        _startPos = (Vector2)transform.localPosition;
        _destinationPos = newPos;
        
        platformScript.moving = true;
        platformScript.speed = speed * platformRepulseMp;
    }

    private Vector2 _gameStartPos = Vector2.zero;
    
    [SerializeField] private float speed = 1;
    
    private Vector2 _startPos;
    private Vector2 _destinationPos;
    private float _lerpT = 2f;
    void Update()
    {
        if (_lerpT < 2f)
        {
            var lerped = Vector2.Lerp(_startPos, _destinationPos, _lerpT);
            transform.localPosition = lerped;
            
            _lerpT += Time.deltaTime * (speed / Vector3.Distance(_startPos, _destinationPos));

            if (lerped == _destinationPos)
            {
                _lerpT = 2f;
                platformScript.moving = false;
            }
                
        }
    }

    public override void Reset()
    {
        transform.localPosition = _gameStartPos;
        _lerpT = 2f;
        platformScript.moving = false;
    }
}
