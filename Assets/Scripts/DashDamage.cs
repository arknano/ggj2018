﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDamage : MonoBehaviour {

    public int Damage;
    public float CollisionSphereRadius;
    public TeleportSO TeleportSO;
    public AudioClip KillSound;
    public float frameFreezeDuration;
    public GameObject damageParticles;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (TeleportSO.IsTeleporting)
        {
            bool showDamage = false;
            var collisions = Physics.OverlapSphere(transform.position, CollisionSphereRadius);
            for (int i = 0; i < collisions.Length; i++)
            {
                if (collisions[i].GetComponent<EnemyHealth>())
                {
                    showDamage = true;
                    collisions[i].GetComponent<EnemyHealth>().DealDamage(Damage);
                    _audioSource.PlayOneShot(KillSound, 1f);
                }
            }
            if (showDamage)
            {
                frameFreeze();
                showDamageParticles();
            }
        }
    }

    void frameFreeze()
    {
        StartCoroutine(SlowTimeScale());
    }

    void showDamageParticles()
    {
        if (damageParticles != null)
        {
            GameObject.Instantiate(damageParticles, transform.position, transform.rotation);
        }
    }

    IEnumerator SlowTimeScale()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(frameFreezeDuration);
        Time.timeScale = 1;
    }
}
