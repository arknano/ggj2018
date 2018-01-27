using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    private KillCounter killCounter;

    public void Start()
    {
        GameObject killCounterObject = GameObject.FindGameObjectWithTag("Kill Counter");
        if (killCounterObject != null)
        {
            killCounter = killCounterObject.GetComponent<KillCounter>();
        }
    }

	public void DealDamage(int damage)
    {
        if (killCounter != null)
        {
            killCounter.EnemyKilled(gameObject);
        }
        Destroy(gameObject);
    }
}
