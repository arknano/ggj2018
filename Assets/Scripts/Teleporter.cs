using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    public float SpawnForce;
    public TeleportSO TeleportSO;
    private Rigidbody _rb;

    // When throwing another teleporter, set this to false
    [HideInInspector]
    public bool isNewestTeleporter = true;

    // Use this for initialization
    void Start () {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(transform.forward * SpawnForce, ForceMode.Impulse);
        TeleportSO.CanTeleport = false;
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Level") && isNewestTeleporter)
        {
            _rb.velocity = Vector3.zero;
            _rb.isKinematic = true;
            _rb.GetComponent<Collider>().enabled = false;
            TeleportSO.CanTeleport = true;
            TeleportSO.TeleportPosition = transform.position + (Vector3.up * 1);
            Destroy(gameObject);
        }
    }
}
