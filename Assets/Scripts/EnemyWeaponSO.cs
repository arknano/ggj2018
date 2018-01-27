using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]

public class EnemyWeaponSO : ScriptableObject {
    public enum Type
    {
        Projectile, HitScan
    }

    [Header(header: "General")]
    public Type type;
    public float reloadTime;
    public float accuracyRadius;
    public GameObject bullet;
    public float attackDamage;
    public AudioClip sound;

    [Header(header: "Projectile Only")]
    public float projectileSpeed;
}
