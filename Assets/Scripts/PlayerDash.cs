using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;
using UnityEngine.Rendering.PostProcessing;

public class PlayerDash : MonoBehaviour {

    public float DashSpeed;
    public GameObject TeleporterPrefab;
    public GameObject PlayerMesh;
    public Transform Barrel;
    public float DashTime;
    public TeleportSO TeleportSO;
    public FOVKick FOVKick = new FOVKick();
    public PostProcessVolume PPBlur;
    public GameObject[] DashTrails;
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
        for (int i = 0; i < DashTrails.Length; i++)
        {
            DashTrails[i].SetActive(true);
        }
        LeanTween.value(gameObject, 0, 1, DashSpeed).setOnUpdate((float val) => { PPBlur.weight = val; });
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
        LeanTween.value(gameObject, 1, 0, DashSpeed).setOnUpdate((float val) => { PPBlur.weight = val; });
        for (int i = 0; i < DashTrails.Length; i++)
        {
            DashTrails[i].SetActive(false);
        }
    }
}
