using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour
{
    LineRenderer lineRenderer;
    public void Awake()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }
    public void AssignConnection(Transform start, Transform end)
    {
        lineRenderer.SetPosition(0, start.position);
        lineRenderer.SetPosition(1, end.position);
    }
}
