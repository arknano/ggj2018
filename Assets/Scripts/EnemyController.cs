using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public EnemyMovementSO movementConfig;
    public EnemyWeaponSO weaponConfig;
    public Transform weaponSpawn;
    public Transform mesh;

    private Transform target;
    private Vector3 moveTarget = Vector3.zero;
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

        bool newPositionFound = determineNewPlayerPosition(out moveTarget);
        if (newPositionFound)
        {
            TryLookAtPlayer(distanceToTarget);
            TryMoveToPlayer(distanceToTarget);
            TryShoot(distanceToTarget);
        }
    }

    void TryLookAtPlayer(float distanceToTarget)
    {
        if (distanceToTarget < movementConfig.sightDistance)
        {
            Vector3 lookDir = target.position - weaponSpawn.position;
            Quaternion targetRotation = Quaternion.LookRotation(lookDir, transform.up);
            mesh.transform.rotation = Quaternion.Lerp(mesh.transform.rotation, targetRotation,
                Time.fixedDeltaTime * movementConfig.turnSpeed);
        }
    }

    void TryMoveToPlayer(float distanceToTarget)
    {
        if (distanceToTarget <= movementConfig.sightDistance &&
            distanceToTarget >= movementConfig.stopApproachingDistance)
        {
            agent.destination = moveTarget;
            agent.speed = movementConfig.speed;
            agent.isStopped = false;
        }
    }

    void TryShoot(float distanceToTarget)
    {
        if (weaponConfig != null && distanceToTarget <= movementConfig.attackDistance)
        {
            if (Time.time - lastShootTime >= weaponConfig.reloadTime)
            {
                lastShootTime = Time.time;
                if (weaponConfig.type == EnemyWeaponSO.Type.Projectile)
                {
                    ShootProjectile();
                }
                else if (weaponConfig.type == EnemyWeaponSO.Type.HitScan)
                {
                    ShootHitscan();
                }
            }
        }
    }

    void ShootProjectile()
    {
        Vector3 random = new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y, 0)
                * weaponConfig.accuracyRadius;
        GameObject bullet = GameObject.Instantiate(weaponConfig.bullet, weaponSpawn);
        bullet.transform.Rotate(random);
        ProjectileController projectileController = bullet.GetComponent<ProjectileController>();
        projectileController.speed = weaponConfig.projectileSpeed;
        projectileController.damage = weaponConfig.attackDamage;
        bullet.transform.parent = null;
    }

    void ShootHitscan()
    {
        Vector3 random = new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y, 0)
                * weaponConfig.accuracyRadius;
        Vector3 laserDir = weaponSpawn.forward + random;
        Vector3 laserStart = weaponSpawn.position;
        Vector3 laserEnd = laserStart + laserDir * movementConfig.attackDistance;

        Debug.DrawLine(laserStart, laserEnd);

        if (weaponConfig.bullet != null)
        {
            GameObject laser = GameObject.Instantiate(weaponConfig.bullet, weaponSpawn.position, weaponSpawn.rotation);
            LaserController laserController = laser.GetComponent<LaserController>();
            laserController.positions = new Vector3[] { laserStart, laserEnd };
        }

        RaycastHit hit;
        if (Physics.Raycast(laserStart, laserDir, out hit, movementConfig.attackDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                target.GetComponent<PlayerHealth>().DealDamage(weaponConfig.attackDamage);
            }
        }
    }

    bool determineNewPlayerPosition(out Vector3 moveTarget)
    {
        RaycastHit hit;
        Vector3 rayDir = (target.position - weaponSpawn.position).normalized;
        if (Physics.Raycast(weaponSpawn.position, rayDir.normalized, out hit))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                moveTarget = target.position;
                return true;
            }
        }
        moveTarget = Vector3.zero;
        return false;
    }
}
