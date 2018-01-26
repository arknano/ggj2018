﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;

public class PlayerDash : MonoBehaviour {

    public float DashSpeed;
    public GameObject TeleporterPrefab;
    public GameObject PlayerMesh;
    public Transform Barrel;
    public float DashTime;
    public TeleportSO TeleportSO;
    public FOVKick FOVKick = new FOVKick();
    private bool _isDashing;
    private Vector3 _dashPoint;
    private FirstPersonController FPSController;

	void Start ()
    {
        FPSController = GetComponent<FirstPersonController>();
        FOVKick.Setup(GetComponentInChildren<Camera>());
	}

	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Dash();
        }
	}

    void Shoot()
    {
        var teleporter = Instantiate(TeleporterPrefab, Barrel.position, Barrel.rotation);
    }

    void Dash()
    {
       if (TeleportSO.CanTeleport)
        {
            TeleportSO.CanTeleport = false;
            StartCoroutine(Dashing());
        }
        
    }

    IEnumerator Dashing()
    {
        StartCoroutine(FOVKick.FOVKickUp());
        FPSController.enabled = false;
        TeleportSO.IsTeleporting = true;
        PlayerMesh.transform.parent = null;
        LeanTween.move (PlayerMesh, TeleportSO.TeleportPosition, DashSpeed);
        yield return new WaitForSeconds(DashTime);
        LeanTween.move(transform.gameObject, TeleportSO.TeleportPosition, DashSpeed);
        TeleportSO.IsTeleporting = false;
        yield return new WaitForSeconds(DashSpeed);
        PlayerMesh.transform.parent = transform;
        FPSController.enabled = true;
        StartCoroutine(FOVKick.FOVKickDown());
    }
}
