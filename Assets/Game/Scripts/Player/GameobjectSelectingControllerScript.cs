using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System.Linq;

public class GameobjectSelectingControllerScript : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] SelectionOutlineController _selectionOutlineController;
    [SerializeField] PlatformEditorWidgetScript _platformEditorWidget;
    [SerializeField] LineRenderer _connectingLineRenderer;
    [SerializeField] SignalLineScript _signalLineOriginal;

    BasePlatformScript _selectedPlatform;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Pressed left click, casting ray.");Script
            CastSelectRay();
        }
        else if (Input.GetMouseButton(0))
        {
            CastMoveRay();
        }
        else
        {
            _moving = false;
        }

        if (Input.GetMouseButtonDown(2))
        {
            CastBeginConnectRay();
        }

        if (Input.GetMouseButtonUp(2))
        {
            //Debug.Log(_connectingReceiver + " <<-> " + _connectingTransmitter);
            
            CastEndConnectRay();
        }

        if (Input.GetMouseButton(2))
        {
            if (_connecting)
            {
                _connectingLineRenderer.SetPosition(1, _camera.ScreenToWorldPoint(Input.mousePosition).WithZ(0));
            }
        }
        else
        {
            EndConnecting();
        }
    }

    #region connecting

    SignalTransmitterScript _connectingTransmitter;
    SignalReceiverScript _connectingReceiver;
    bool _connecting;

    private void Connect(SignalTransmitterScript transmitter, SignalReceiverScript receiver)
    {
        if (receiver.IsConnected(transmitter))
        {
            receiver.Disconnect(transmitter);
        }
        else
        {
            var line = Instantiate(_signalLineOriginal);
            line.Init(transmitter, receiver);

            receiver.Connect(transmitter, line);
        }
    }

    void BeginConnecting()
    {
        _connecting = true;
        _connectingLineRenderer.SetPosition(0, _camera.ScreenToWorldPoint(Input.mousePosition).WithZ(0));
    }

    void EndConnecting()
    {
        if (_connecting)
        {
            _connectingLineRenderer.SetPosition(0, Vector3.back);
            _connectingLineRenderer.SetPosition(1, Vector3.back);
        }

        _connecting = false;
    }

    void CastEndConnectRay()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        var res = Physics2D.Raycast(ray.origin, ray.direction);

        if (res.collider)
        {
            SignalReceiverScript rec = null;
            if (res.collider.TryGetComponent<SignalReceiverScript>(out var r))
            {
                rec = r;
            }
            else
            {
                rec = res.collider.GetComponentInParent<SignalReceiverScript>();
            }
            _connectingReceiver = rec;



            if (_connectingReceiver && _connectingTransmitter)
            {
                Connect(_connectingTransmitter, _connectingReceiver);
            }
        }
    }

    void CastBeginConnectRay()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        var res = Physics2D.Raycast(ray.origin, ray.direction);

        if (res.collider)
        {
            var tr = res.collider.GetComponentInParent<SignalTransmitterScript>();
            if (tr)
            {
                _connectingTransmitter = tr;
                BeginConnecting();
            }
            else
            {
                
            }
            
            //print((tr));
        }
        else
        {
            
        }

    }
    #endregion

    #region moving

    bool _moving;
    Vector3 _movingOffset;

    void CastMoveRay()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        var res = Physics2D.Raycast(ray.origin, ray.direction);

        if (res.collider || (_moving && _selectedPlatform != null))
        {
            if (_moving || IsGameObjectSelected(res.collider.gameObject))
            {
                var mouseInWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition).WithZ(0);

                if (!_moving)
                {
                    _movingOffset = _selectedPlatform.platformParams.position.ToVec3(0) - mouseInWorldPos;
                    _moving = true;
                }
                    

                _selectedPlatform.platformParams.position = (mouseInWorldPos + _movingOffset).Round(0.5f);
                _selectedPlatform.ApplyParams();
            }
            else
            {
                _moving = false;
            }
        }
        else
        {
            _moving = false;
        }
    }
    #endregion

    #region selecting
    void CastSelectRay()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        //Debug.DrawLine(ray.origin, ray.direction * 100);

        var res = Physics2D.Raycast(ray.origin, ray.direction);

        if (_platformEditorWidget.IsMouseInSaveZone())
            return;

        if (res.collider)
        {
            //Debug.DrawLine(ray.origin, res.point);
            //Debug.Log("Hit object: " + res.collider.gameObject.name);

            //Debug.Log(res.transform);

            Select(res.collider.gameObject);

            //_selectionOutlineController.OutlineGameobject(res.collider.gameObject);
        }
        else
        {
            _selectionOutlineController.ClearOutline();
            _selectedPlatform = null;
            _platformEditorWidget.SetupPlatform(null);
        }
    }

    public bool IsGameObjectSelected(GameObject go)
    {
        if(_selectedPlatform)
            return _selectedPlatform.GetComponentsInChildren<Transform>().Any(x => x.gameObject == go);
        return false;
    }


    private void Select(GameObject go)
    {
        var obj = go.GetComponentInParent<BasePlatformScript>();
        if (obj != null)
        {
            _selectionOutlineController.OutlineGameobject(go);
            _platformEditorWidget.SetupPlatform(obj);
            _selectedPlatform = obj;
        }
        //Debug.Log("P://" + obj.name);
    }
    #endregion
}
