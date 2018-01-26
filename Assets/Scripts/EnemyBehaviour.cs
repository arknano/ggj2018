using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour {

    public ProjectileEnemySO enemyConfig;
    public Transform weaponSpawn;
    private Transform target;

    private NavMeshAgent agent;
    private float lastShootTime;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyConfig.walkSpeed;
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
        if (distanceToTarget < enemyConfig.sightDistance)
        {
            agent.destination = target.position;
            agent.speed = enemyConfig.runSpeed;
        }
        else
        {
            agent.isStopped = true;
            agent.speed = enemyConfig.walkSpeed;
        }
    }

    void TryShoot(float distanceToTarget)
    {
        if (distanceToTarget < enemyConfig.attackDistance)
        {
            WeaponSO weapon = enemyConfig.weapon;
            if (Time.time - lastShootTime >= weapon.reloadTime)
            {
                GameObject bullet = GameObject.Instantiate(weapon.projectile, weaponSpawn);
                bullet.transform.parent = null;
                lastShootTime = Time.time;
            }
        }
    }
}
