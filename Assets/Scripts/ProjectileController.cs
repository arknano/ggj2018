using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

    public float damage;
    public float speed;
    public bool Explodes;
    public float ExplosionRadius;
    public GameObject ExplosionPrefab;

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
        if (collision.gameObject.CompareTag("Level"))
        {
            if (Explodes)
            {
                Instantiate(ExplosionPrefab, collision.contacts[0].point, Quaternion.identity);
                var collisions = Physics.OverlapSphere(transform.position, ExplosionRadius);
                for (int i = 0; i < collisions.Length; i++)
                {
                    if (collisions[i].gameObject.CompareTag("Player"))
                    {
                        collisions[i].GetComponent<PlayerHealth>().DealDamage(damage);
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
