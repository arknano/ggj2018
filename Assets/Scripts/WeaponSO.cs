using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]

public class WeaponSO : ScriptableObject {
    public enum Type
    {
        Projectile, HitScan
    }

    public Type type;
    public float reloadTime;
    public float accuracyRadius;
    public GameObject projectile;
}
