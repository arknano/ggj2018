using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour {

    public float DashSpeed;
    public GameObject TeleporterPrefab;
    public GameObject PlayerMesh;
    public Transform Barrel;
    public float DashTime;
    public TeleportSO TeleportSO;
    private bool _isDashing;
    private Vector3 _dashPoint;

	void Start () {
		
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
        PlayerMesh.transform.parent = null;
        LeanTween.move(PlayerMesh, TeleportSO.TeleportPosition, DashSpeed);
        yield return new WaitForSeconds(DashTime);
        LeanTween.move(transform.gameObject, TeleportSO.TeleportPosition, DashSpeed);
        yield return new WaitForSeconds(DashSpeed);
        PlayerMesh.transform.parent = transform;
    }
}
