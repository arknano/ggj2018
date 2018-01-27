using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoorController : MonoBehaviour {

    public GameObject Door;

	public void Open()
    {
        Door.SetActive(true);
    }
}
