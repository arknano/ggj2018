using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TeleportSO : ScriptableObject {

    public Vector3 TeleportPosition;
    public bool CanTeleport;
    public bool IsTeleporting;
}
