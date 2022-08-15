using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsiveObjScript : MonoBehaviour
{
    public float strength = 100;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.rigidbody.AddForce(-collision.contacts[0].normal * strength);
        Debug.Log("Added");
    }
}
