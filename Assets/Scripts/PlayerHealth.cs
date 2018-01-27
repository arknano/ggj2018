using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int StartingHealth;
    public FloatVariable PlayerHP;
    public float HealthRegenTimer;
    public float HealthRegenSpeed;
    private float _regenTime;
    private bool _regen;

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
            _regen = false;
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

    private void Update()
    {
        if (!_regen)
        {
            _regenTime += Time.deltaTime;
        }
        if (_regenTime > HealthRegenTimer)
        {
            _regen = true;
            _regenTime = 0;
        }
        if (_regen)
        {
            PlayerHP.Value += Time.deltaTime * HealthRegenSpeed;
            if (PlayerHP.Value > 100)
            {
                _regen = false;
                PlayerHP.Value = 100;
            }
        }
    }
}
