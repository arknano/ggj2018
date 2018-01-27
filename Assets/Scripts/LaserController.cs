using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {

    public float duration;
    public Vector3[] positions;

    void Start () {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
        Destroy(gameObject, duration);
    }
}
