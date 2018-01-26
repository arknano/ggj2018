using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDamage : MonoBehaviour {

    public int Damage;
    public float CollisionSphereRadius;
    public TeleportSO TeleportSO;

    private void Update()
    {
        if (TeleportSO.IsTeleporting)
        {
            var collisions = Physics.OverlapSphere(transform.position, CollisionSphereRadius);
            for (int i = 0; i < collisions.Length; i++)
            {
                if (collisions[i].GetComponent<EnemyHealth>())
                {
                    collisions[i].GetComponent<EnemyHealth>().DealDamage(Damage);
                }
            }
        }
    }
}
