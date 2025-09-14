using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]

public class FishingLineBobber : MonoBehaviour
{
    [HideInInspector] public Transform startPoint;
    [HideInInspector] public Transform endPoint;

    public int segmentCount = 20;
    public float curveHeight = 2f;

    private LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = segmentCount;
    }

    void Update()
    {
        if (startPoint != null && endPoint != null)
        {
            DrawCurve();
        }
    }

    void DrawCurve()
    {
        for (int i = 0; i < segmentCount; i++)
        {
            float t = i / (float)(segmentCount - 1);

            Vector3 pos = Vector3.Lerp(startPoint.position, endPoint.position, t);

            float parabola = Mathf.Sin(t * Mathf.PI) * curveHeight;
            pos.y -= parabola;

            lr.SetPosition(i, pos);
        }
    }
}


