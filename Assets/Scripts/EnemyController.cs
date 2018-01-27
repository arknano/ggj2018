using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public EnemyMovementSO movementConfig;
    public EnemyWeaponSO weaponConfig;
    public Transform[] weaponSpawns;
    public Transform mesh;

    private enum MovementType
    {
        ForwardBack, Strafe
    }

    private Transform target;
    private Vector3 moveTarget = Vector3.zero;
    private NavMeshAgent agent;
    private float lastShootTime;
    private float lastMovementChangeTime;
    private float nextMovementChangeTime;
    private MovementType movementType = MovementType.Strafe;
    private float strafeRadius;
    private bool moveLeft = false;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementConfig.speed;
        lastShootTime = Time.time;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        lastMovementChangeTime = Time.time;
        nextMovementChangeTime = lastMovementChangeTime + movementConfig.maxTimeBetweenMovementChange;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float distanceToTarget = (target.position - transform.position).magnitude;

        bool newPositionFound = DetermineNewPlayerPosition(out moveTarget);
        if (newPositionFound)
        {
            // Attack the player (or strafe)
            LookAtTarget(distanceToTarget, target.position);
            Move(distanceToTarget);
            TryShoot(distanceToTarget);
        }
    }

    void LookAtTarget(float distanceToTarget, Vector3 lookTarget)
    {
        if (distanceToTarget < movementConfig.sightDistance)
        {
            Vector3 lookDir = lookTarget - weaponSpawns[0].position;
            Quaternion targetRotation = Quaternion.LookRotation(lookDir, transform.up);
            mesh.transform.rotation = Quaternion.Lerp(mesh.transform.rotation, targetRotation,
                Time.fixedDeltaTime * movementConfig.turnSpeed);
        }
    }

    void Move(float distanceToTarget)
    {
        if (Time.time > nextMovementChangeTime) 
        {
            lastMovementChangeTime = Time.time;
            movementType = Random.value > 0.5f ? MovementType.Strafe : MovementType.ForwardBack;
            moveLeft = Random.value > 0.5f;
            strafeRadius = (transform.position - target.position).magnitude;
            nextMovementChangeTime = Time.time +
                Random.Range(movementConfig.maxTimeBetweenMovementChange / 2,
                movementConfig.maxTimeBetweenMovementChange);
        }

        if (!movementConfig.canStrafe)
        {
            movementType = MovementType.ForwardBack;
        }

        switch (movementType) {
            case MovementType.ForwardBack:
                MoveForwardBack(distanceToTarget);
                break;
            case MovementType.Strafe:
                MoveStrafe(distanceToTarget);
                break;
        }
    }

    void MoveForwardBack(float distanceToTarget)
    {
        if (distanceToTarget <= movementConfig.sightDistance)
        {
            if (distanceToTarget < movementConfig.stopApproachingDistance - 1)
            {
                FleeFromPlayer();
            } else
            {
                MoveToPlayer();
            }
        }
    }

    void MoveStrafe(float distanceToTarget)
    {
        Vector3 fromPlayerDir = (transform.position - target.position).normalized;

        float angle = 25 * (moveLeft ? 1 : -1);
        Vector3 fromPlayerAngled = Quaternion.AngleAxis(angle, Vector3.up) * fromPlayerDir;
        Vector3 strafeDirection = fromPlayerAngled - fromPlayerDir;
        Vector3 strafeTo = transform.position + strafeDirection * 5;

        // Strafe and move backwards if near player
        if (distanceToTarget < movementConfig.attackDistance / 2)
        {
            float moveAmount = movementConfig.attackDistance - distanceToTarget;
            strafeTo += fromPlayerDir * movementConfig.attackDistance;
        }

        Vector3 resultStrafeTo;
        if (NavMeshTo(strafeTo, 5, out resultStrafeTo))
        {
            agent.destination = resultStrafeTo;
        }
        else
        {
            moveLeft = !moveLeft;
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
        foreach (Transform weaponSpawn in weaponSpawns) {
            Vector3 random = new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y, 0)
                    * weaponConfig.accuracyRadius;
            GameObject bullet = GameObject.Instantiate(weaponConfig.bullet, weaponSpawn);
            bullet.transform.Rotate(random);
            ProjectileController projectileController = bullet.GetComponent<ProjectileController>();
            projectileController.speed = weaponConfig.projectileSpeed;
            projectileController.damage = weaponConfig.attackDamage;
            bullet.transform.parent = null;
        }
    }

    void ShootHitscan()
    {
        Vector3 random = new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y, 0)
                * weaponConfig.accuracyRadius;
        Vector3 laserDir = weaponSpawns[0].forward + random;
        Vector3 laserStart = weaponSpawns[0].position;
        Vector3 laserEnd = laserStart + laserDir * 2000;

        RaycastHit hit;
        if (Physics.Raycast(weaponSpawns[0].position, laserDir, out hit))
        {
            laserEnd = hit.point;
        }

        if (weaponConfig.bullet != null)
        {
            GameObject laser = GameObject.Instantiate(weaponConfig.bullet, weaponSpawns[0].position, weaponSpawns[0].rotation);
            LaserController laserController = laser.GetComponent<LaserController>();
            laserController.positions = new Vector3[] { laserStart, laserEnd };
        }

        if (Physics.Raycast(laserStart, laserDir, out hit, movementConfig.attackDistance))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                target.GetComponent<PlayerHealth>().DealDamage(weaponConfig.attackDamage);
            }
        }
    }

    bool DetermineNewPlayerPosition(out Vector3 moveTarget)
    {
        RaycastHit hit;
        Vector3 rayDir = (target.position - weaponSpawns[0].position).normalized;
        if (Physics.Raycast(weaponSpawns[0].position, rayDir.normalized, out hit))
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

    private void MoveToPlayer()
    {
        agent.destination = moveTarget;
        agent.isStopped = false;
    }

    private void FleeFromPlayer()
    {
        float distance = movementConfig.stopApproachingDistance
                - (target.position - transform.position).magnitude;
        Vector3 runTo = transform.position - (target.position - transform.position) * distance;

        Vector3 resultRunTo;
        if (NavMeshTo(runTo, distance, out resultRunTo)) {
            agent.destination = resultRunTo;
            agent.isStopped = false;
        }
    }

    private bool NavMeshTo(Vector3 targetPoint, float dist, out Vector3 result)
    {
        NavMeshHit hit;
        int mask = 1 << NavMesh.GetAreaFromName("Walkable");
        if (NavMesh.SamplePosition(targetPoint, out hit, dist, mask))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }
}
