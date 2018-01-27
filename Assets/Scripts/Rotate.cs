using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public Vector3 Rotation;
    public bool RandomRotation;
    public float MaxRandomSpeed;

    private Vector3 _randomRotation;

    private void Start()
    {
        _randomRotation = Random.rotation.eulerAngles;
    }

    private void Update()
    {
        if (RandomRotation)
        {
            transform.Rotate(_randomRotation * Time.deltaTime * MaxRandomSpeed);
        }
        else
        {
            transform.Rotate(Rotation * Time.deltaTime, Space.World);
        }
    }

}
