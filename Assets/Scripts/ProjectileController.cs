using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float damage;
    public float speed;

    void Start () {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = speed * transform.forward;
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerHealth>())
            {
                collision.gameObject.GetComponent<PlayerHealth>().DealDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
