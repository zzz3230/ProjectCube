using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class TrajectorySimulatorScript : MonoBehaviour
{
    public float simulateTime = 3;

    [SerializeField] private Transform cubeInstanceTransform;

    private Rigidbody2D _cubeInstanceRb; 
    
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform endTransform;
    
    private float _elapsed;
    private bool _simulating;

    private void Start()
    {
        _cubeInstanceRb = cubeInstanceTransform.GetComponent<Rigidbody2D>();
        Time.timeScale = 1;
        StopSimulating();
    }

    [Button]
    public void Simulate()
    {
        TestScene_WorldScript.ResetAll();
        StartSimulating();
    }

    public void StopSimulating()
    {
        TestScene_WorldScript.ResetAll();

        _simulating = false;
        cubeInstanceTransform.GetComponent<TrajectoryDrawerScript>().Stop();
        cubeInstanceTransform.position = endTransform.position;
        _cubeInstanceRb.simulated = false;
    }

    void StartSimulating()
    {
        _simulating = true;
        _elapsed = 0f;
        
        cubeInstanceTransform.position = startTransform.position;
        cubeInstanceTransform.rotation = startTransform.rotation;
        
        _cubeInstanceRb.velocity = Vector2.zero;
        _cubeInstanceRb.angularVelocity = 0;
        
        _cubeInstanceRb.simulated = true;
        
        cubeInstanceTransform.GetComponent<TrajectoryDrawerScript>().Clear();
        cubeInstanceTransform.GetComponent<TrajectoryDrawerScript>().Resume();
    }
    void Update()
    {
        if (_simulating)
        {
            if (_elapsed > simulateTime)
            {
                StopSimulating();
            }

            _elapsed += Time.deltaTime;
        }
    }
}
