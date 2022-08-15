using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class UIMouseDetectorScript : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
    RectTransform _transform;
    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //Debug.Log("rect: "+_transform.rect + " mpos: " + Input.mousePosition + " isIN: " + _transform.rect.Contains(Input.mousePosition));
        
    }

    public bool isMouseIn { 
        get => RectTransformUtility.RectangleContainsScreenPoint(_transform, Input.mousePosition); 
    //    private set; 
    }

    /*public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("IN");
        isMouseIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OUT");
        isMouseIn = false;
    }?*/
}
