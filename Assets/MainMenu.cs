using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public string SceneName;

    private void Start()
    {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
