using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFrictionScript : MonoBehaviour
{
    [SerializeField]
    Vector3 _pos, _velocity;

    [SerializeField] private float mp = 1;
    
    void Awake()
    {
        _pos = transform.position;
    }

    void Update()
    {
        _velocity = (transform.position - _pos) / Time.deltaTime;
        _pos = transform.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        var vel = collision.rigidbody.velocity;
        vel.x += (_velocity * mp).x;
        collision.rigidbody.velocity = vel;
    }
}
