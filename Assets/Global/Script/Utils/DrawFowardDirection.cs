using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawFowardDirection : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField]
    private float visionLength = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;

        lineRenderer.startWidth = 0.1f;

        lineRenderer.endWidth = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.forward * visionLength + transform.position);
    }
}
