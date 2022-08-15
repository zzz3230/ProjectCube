using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlatformScript : MonoBehaviour, IPointerClickHandler
{
    public bool moving;
    public float speed;
    

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (moving)
        {
            if (collision.rigidbody)
            {
                collision.rigidbody.AddForce(collision.contacts[0].normal * speed);
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        print(eventData.button);
    }
}
