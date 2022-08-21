// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AngleFieldInputRotatorScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float angle { get; private set; }
    public event System.Action<float> angleChanged;

    //.transform
    bool _input;
    
    public bool forceInput;
    public Vector2 forceInputAround;

    public void OnPointerDown(PointerEventData eventData)
    {
        _input = true;
    }

    internal void SetAngle(float v)
    {
        //Debug.Log(gameObject);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.WithZ(v));
        //throw new System.NotImplementedException();)
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _input = false;
    }

    private void Update()
    {
        if (_input || forceInput)
        {

            float y1 = forceInput ? forceInputAround.x : transform.position.x; 
            float x1 = forceInput ? forceInputAround.y : transform.position.y;

            Vector3 mPos = Input.mousePosition;

            float y0 = mPos.x;
            float x0 = mPos.y;

            var radAngle = Mathf.Atan2((y1 - y0), (x1 - x0));

            angle = -radAngle * Mathf.Rad2Deg + 90;

            //Debug.Log("before: " + angle);else

            if (angle > 265)
                angle = 270;

            angle = angle - angle % 15;

            

            //Debug.Log("after: " + angle);

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.WithZ(angle));

            angleChanged?.Invoke(angle);
        }
    }
}
