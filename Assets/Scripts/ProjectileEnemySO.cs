using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class ProjectileEnemySO : ScriptableObject {
    public float walkSpeed;
    public float runSpeed;
    public float sightDistance;
    public float attackDistance;
}
