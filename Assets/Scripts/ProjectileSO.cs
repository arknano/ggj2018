using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Projectile")]
public class ProjectileSO : ScriptableObject {
    public float speed;
    public float damage;
    public GameObject model;
}
