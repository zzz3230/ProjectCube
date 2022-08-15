using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryDrawerScript : MonoBehaviour
{
    public float pointEvery = 0.1f;

    private float _elapsed;

    private LineRenderer _lineRenderer;

    private bool _drawing = true;
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        AppendPoint();
    }

    public void Clear()
    {
        _positions.Clear();
        _lineRenderer.positionCount = 0;
    }
    
    public void Stop()
    {
        _drawing = false;
    }

    public void Resume()
    {
        _drawing = true;
    }
    void Update()
    {
        if(!_drawing)
            return;
        
        if (_elapsed > pointEvery)
        {
            AppendPoint();
            _elapsed = 0;
        }

        _elapsed += Time.deltaTime;
    }

    private List<Vector3> _positions = new List<Vector3>(); 
    private void AppendPoint()
    {
        _positions.Add(transform.position);
        _lineRenderer.positionCount = _positions.Count;
        _lineRenderer.SetPositions(_positions.ToArray());
    }
}
