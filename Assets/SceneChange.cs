using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    public float FadeTime;
    public PostProcessVolume PPBloom;
    public CanvasGroup WhiteOut;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }
    
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(SceneChanger(sceneName));
    }

    IEnumerator SceneChanger (string sceneName)
    {
        LeanTween.value(gameObject, 0, 1, FadeTime).setOnUpdate((float val) => { PPBloom.weight = val; });
        yield return new WaitForSeconds(FadeTime);
        LeanTween.alphaCanvas(WhiteOut, 1, FadeTime);
        yield return new WaitForSeconds(FadeTime);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator FadeIn ()
    {
        LeanTween.alphaCanvas(WhiteOut, 0, FadeTime);
        LeanTween.value(gameObject, 1, 0, FadeTime).setOnUpdate((float val) => { PPBloom.weight = val; });
        yield return new WaitForEndOfFrame();
    }
}
