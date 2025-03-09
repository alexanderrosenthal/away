using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobeController : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineRenderer lr;

    private void Start()
    {
        SetUpLine(points);
    }

    public void SetUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }
    private void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }
}