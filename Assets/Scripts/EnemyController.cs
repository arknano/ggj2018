using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public EnemyMovementSO movementConfig;
    public EnemyWeaponSO weaponConfig;
    public Transform weaponSpawn;

    private Transform target;
    private NavMeshAgent agent;
    private float lastShootTime;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementConfig.speed;
        lastShootTime = Time.time;
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float distanceToTarget = (target.position - transform.position).magnitude;

        TryMove(distanceToTarget);
        TryShoot(distanceToTarget);
    }

    void TryMove(float distanceToTarget)
    {
        if (distanceToTarget < movementConfig.sightDistance)
        {
            agent.destination = target.position;
            agent.speed = movementConfig.speed;
        }
        else
        {
            agent.isStopped = true;
            agent.speed = movementConfig.speed;
        }
    }

    void TryShoot(float distanceToTarget)
    {
        if (distanceToTarget < movementConfig.attackDistance)
        {
            if (Time.time - lastShootTime >= weaponConfig.reloadTime)
            {
                GameObject bullet = GameObject.Instantiate(weaponConfig.projectile, weaponSpawn);
                bullet.transform.parent = null;
                lastShootTime = Time.time;
            }
        }
    }
}
