using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceleratingObjScript : MonoBehaviour
{
    public float strength = 10;
    private void OnCollisionStay2D(Collision2D collision)
    {
        var newVel = collision.rigidbody.velocity * strength;

        newVel.x = Mathf.Clamp(newVel.x, -30, 30);
        newVel.y = Mathf.Clamp(newVel.y, -30, 30);
       
        collision.rigidbody.velocity = newVel;
    }
}
