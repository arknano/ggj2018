using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    public ProjectileSO projectileConfig;

    void Start () {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = projectileConfig.speed * transform.forward;
	}
}
