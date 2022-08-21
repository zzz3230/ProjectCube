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
    [SerializeField] LevelBuilderScript _levelBuilder;

    BasePlatformScript _selectedPlatform;


    void Update()
    {
        if (!LevelScript.instance.canEditLevel)
            return;

        if(_selectedPlatform && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
        {
            var newParams = Input.GetKey(KeyCode.RightShift) ? _selectedPlatform.platformParams : _selectedPlatform.platformParams.Dublicate();
            _levelBuilder.BuildInCenter(_selectedPlatform.prefab.buildingID, newParams);
        }
            

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
    public bool ConnectObjects(GameObject transmitter, GameObject receiver)
    {
        if (TryBeginConnecting(transmitter))
            if (TryEndConnecting(receiver))
                return true;
        return false;
    }


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

    bool TryEndConnecting(GameObject obj)
    {
        SignalReceiverScript rec = null;
        if (obj.TryGetComponent<SignalReceiverScript>(out var r))
        {
            rec = r;
        }
        else
        {
            rec = obj.GetComponentInParent<SignalReceiverScript>();
        }
        _connectingReceiver = rec;



        if (_connectingReceiver && _connectingTransmitter)
        {
            Connect(_connectingTransmitter, _connectingReceiver);
            return true;
        }
        return false;
    }

    void CastEndConnectRay()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        var res = Physics2D.Raycast(ray.origin, ray.direction);

        if (res.collider)
        {
            TryEndConnecting(res.collider.gameObject);
        }
    }

    bool TryBeginConnecting(GameObject obj)
    {
        if(obj.TryGetComponent<SignalTransmitterScript>(out var mtr))
        {
            _connectingTransmitter = mtr;
            return true;
        }

        var tr = obj.GetComponentInParent<SignalTransmitterScript>();
        if (tr)
        {
            _connectingTransmitter = tr;
            return true;
        }
        else
        {
            return false;
        }
    }
    void CastBeginConnectRay()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        var res = Physics2D.Raycast(ray.origin, ray.direction);

        if (res.collider)
        {
            if(TryBeginConnecting(res.collider.gameObject))
                BeginConnecting();
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

                //
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
