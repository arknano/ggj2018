using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Utility;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

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
    public Transform Shatter;
    public float ExplosionForce;
    private bool _isDashing;
    private Vector3 _dashPoint;
    private FirstPersonController FPSController;
    public AudioSource DashSFX;

    public GameObject teleportBeacon;
    private GameObject teleportBeaconInstance;
    private LineRenderer teleportBeamRenderer;
    private Teleporter latestTeleporter = null;

    void Start ()
    {
        FPSController = GetComponent<FirstPersonController>();
        FOVKick.Setup(GetComponentInChildren<Camera>());

        TeleportSO.CanTeleport = false;
        TeleportSO.IsTeleporting = false;
        TeleportSO.TeleportPosition = Vector3.zero;
        teleportBeamRenderer = GetComponent<LineRenderer>();
        teleportBeamRenderer.enabled = false;
        teleportBeaconInstance = Instantiate(teleportBeacon);
        teleportBeaconInstance.SetActive(false);
        TeleportSO.CanTeleport = false;
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

        TryShowTeleportIndicator();
	}

    void Shoot()
    {
        if (latestTeleporter != null)
        {
            latestTeleporter.isNewestTeleporter = false;
        }
        GameObject teleporterObject = Instantiate(TeleporterPrefab, Barrel.position, Barrel.rotation);
        latestTeleporter = teleporterObject.GetComponent<Teleporter>();
    }

    void Dash()
    {
        teleportBeamRenderer.enabled = false;
        teleportBeaconInstance.SetActive(false);

        if (TeleportSO.CanTeleport)
        {
            TeleportSO.CanTeleport = false;
            DashSFX.Play();
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

    private void TryShowTeleportIndicator()
    {
        if (TeleportSO.CanTeleport)
        {
            Vector3 end = TeleportSO.TeleportPosition;
            end.y -= 1;

            teleportBeaconInstance.transform.position = TeleportSO.TeleportPosition;
            teleportBeaconInstance.SetActive(true);
            teleportBeamRenderer.enabled = true;
            teleportBeamRenderer.SetPositions(new Vector3[] { transform.position, end });
            teleportBeamRenderer.positionCount = 2;
        } else {
            teleportBeaconInstance.SetActive(false);
            teleportBeamRenderer.enabled = false;
        }
    }

    public void DeathDash()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        PlayerMesh.transform.parent = null;
        TeleportSO.IsTeleporting = true;
        var pieces = Shatter.GetComponentsInChildren<Rigidbody>();
        PlayerMesh.SetActive(false);
        Shatter.parent = null;
        Shatter.gameObject.SetActive(true);
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].AddExplosionForce(ExplosionForce, transform.position + (Vector3.down * 1), 1f);
        }
        FPSController.enabled = false;
        StartCoroutine(FOVKick.FOVKickUp());
        LeanTween.move(gameObject, transform.position + (-transform.forward * 5) , DashSpeed);
        yield return new WaitForSeconds(3);
        GetComponent<SceneChange>().ChangeScene(SceneManager.GetActiveScene().name);
        GetComponent<CharacterController>().enabled = false;
    }
}
