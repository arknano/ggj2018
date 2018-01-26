using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Movement", menuName = "Enemy Movement")]
public class EnemyMovementSO : ScriptableObject {
    public float speed;
    public float sightDistance;
    public float attackDistance;
}
