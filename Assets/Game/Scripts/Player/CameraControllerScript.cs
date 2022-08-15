using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraControllerScript : MonoBehaviour
{
    [SerializeField] Material _backgroundMaterial;

    private Camera _camera;
    //private Vector3 _startPos;
    //private Vector3 _startWorldPos;
    //private Vector2 _toLerpPos;

    private Vector3 Origin;
    private Vector3 Difference;
    private Vector3 ResetCamera;
    private float _cameraSize;

    [SerializeField] private float _scrollSpeed = 1;
    
    private bool _isDragging;
    private void Start()
    {
        _camera = GetComponent<Camera>();
        ResetCamera = _camera.transform.position;
        _cameraSize = _camera.orthographicSize;
    }
    
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Difference = (_camera.ScreenToWorldPoint(Input.mousePosition)) - _camera.transform.position;
            if (_isDragging == false)
            {
                _isDragging = true;
                Origin = _camera.ScreenToWorldPoint(Input.mousePosition);
            }

        }
        else
        {
            _isDragging = false;
        }

        if (_isDragging)
        {
            _camera.transform.position = Origin - Difference;// * 0.5f;
        }

        //if (Input.GetMouseButton(2))
        //    _camera.transform.position = ResetCamera;

        /*if(Input.GetMouseButtonDown(2))
            BeginDrag();
        else if(Input.GetMouseButtonUp(2))
            EndDrag();

        if (_isDragging)
        {
            Vector3 pos = _startPos - _camera.ScreenToViewportPoint(Input.mousePosition);

            pos *= 15;
            
            _camera.transform.position = _startWorldPos + pos;
            //transform.Translate(, Space.World);  
        }*/


        //Debug.Log(Input.mouseScrollDelta); _camera.orthographicSize

        var ms = Input.mouseScrollDelta.y;

        _cameraSize += -ms * Time.deltaTime * _scrollSpeed;
        _cameraSize = Mathf.Clamp(_cameraSize, 1, 15);
        
        if(_cameraSize != _camera.orthographicSize)
        {
            _camera.orthographicSize = _cameraSize;
            //_backgroundMaterial.SetFloat("_Scale", Mathf.Exp(_cameraSize / 10));
        }
        
    }

    /*void BeginDrag()
    {
        _isDragging = true;
        _startWorldPos = _camera.transform.position;
        _startPos = _camera.ScreenToViewportPoint(Input.mousePosition);
    }

    void EndDrag()
    {
        _isDragging = false;
    }8*(.*/
}
