using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevelPortal : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("MOVE TO NEXT LEVEL HERE");
    }
}
