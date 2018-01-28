using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsTimer : MonoBehaviour {

    public SceneChange sceneChange;

	void Start () {
        StartCoroutine(BackToMenu());
	}
	
	IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(9f);
        sceneChange.ChangeScene("Menu");
    }
}
