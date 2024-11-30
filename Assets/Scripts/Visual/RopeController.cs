using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform[] points;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    void Start()
    {
        SetUpLine(points);
    }
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            if (points[i] != null)
            {
                lineRenderer.SetPosition(i, points[i].position);
            }
            else
            {
                lineRenderer.SetPosition(i, points[0].position);
            }
        }
    }
    void SetUpLine(Transform[] points)
    {
        lineRenderer.positionCount = points.Length;
        this.points = points;
    }
}
