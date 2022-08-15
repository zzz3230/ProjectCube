using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayingObjScript : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        col.rigidbody.velocity = Vector2.zero;
        col.rigidbody.angularVelocity = 0f;
    }
}
