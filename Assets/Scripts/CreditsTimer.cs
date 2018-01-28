using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsTimer : MonoBehaviour {

    public SceneChange sceneChange;

	void Start () {
        StartCoroutine(BackToMenu());
	}
	
	IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(8.9f);
        sceneChange.ChangeScene("Menu");
    }
}
