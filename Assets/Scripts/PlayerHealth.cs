using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int StartingHealth;
    public FloatVariable PlayerHP;

    private void Start()
    {
        PlayerHP.Value = StartingHealth;
    }

    public void DealDamage(float damage)
    {
        PlayerHP.Value -= damage;
        CheckHealth();
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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
