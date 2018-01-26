using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int StartingHealth;
    public FloatVariable PlayerHP;

    private void Start()
    {
        PlayerHP.Value = StartingHealth;
    }

    public void DealDamage(float damage)
    {
        PlayerHP.Value -= damage;
    }
}
