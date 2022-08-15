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
        if (_input)
        {
            float y1 = transform.position.x; 
            float x1 = transform.position.y;

            Vector3 mPos = Input.mousePosition;

            float y0 = mPos.x;
            float x0 = mPos.y;

            var radAngle = Mathf.Atan2((y1 - y0), (x1 - x0));

            angle = -radAngle * Mathf.Rad2Deg + 90;

            angle = angle - angle % 15;

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.WithZ(angle));

            angleChanged?.Invoke(angle);
        }
    }
}
