using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class LogicNodeWidgetScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IMoveHandler
{
    [SerializeField] GameObject draggableArea;

    bool _allowDrag;
    /*
    public void OnBeginDrag(PointerEventData eventData)
    {
        //UnityEngine.Debug.Log("begin dragging");
        if (eventData.pointerCurrentRaycast.gameObject == draggableArea)
            _allowDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //UnityEngine.Debug.Log("dragging");
        if(_allowDrag)
            transform.position = eventData.pointerCurrentRaycast.screenPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //UnityEngine.Debug.Log("end dragging");
        _allowDrag = false;
    }*/

    void Update()
    {
        if (_allowDrag)
            transform.position = Input.mousePosition;
    }

    public void OnMove(AxisEventData eventData)
    {
        //if (_allowDrag)
        //    transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == draggableArea)
            _allowDrag = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _allowDrag = false;
    }
}
