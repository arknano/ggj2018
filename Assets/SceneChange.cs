using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SceneChange : MonoBehaviour {

    public float FadeTime;
    public PostProcessVolume PPBloom;
    public GameObject WhiteOut;

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(SceneChanger(sceneName));
    }

    IEnumerator SceneChanger (string sceneName)
    {
        LeanTween.value(gameObject, 0, 1, FadeTime).setOnUpdate((float val) => { PPBloom.weight = val; });
        yield return new WaitForSeconds(FadeTime);
        LeanTween.alpha(WhiteOut, 1, FadeTime / 2);
        
    }
}
