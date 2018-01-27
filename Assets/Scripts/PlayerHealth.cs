using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int StartingHealth;
    public FloatVariable PlayerHP;

    private DashDamage dashDamage;

    private void Start()
    {
        dashDamage = GetComponentInChildren<DashDamage>();
        PlayerHP.Value = StartingHealth;
    }

    public void DealDamage(float damage)
    {
        if (!dashDamage.TeleportSO.IsTeleporting)
        {
            PlayerHP.Value -= damage;
            CheckHealth();
        }
    }

    void CheckHealth()
    {
        if (PlayerHP.Value <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GetComponent<PlayerDash>().DeathDash();
    }
}
