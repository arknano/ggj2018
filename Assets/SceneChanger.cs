using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour {

    public string SceneName;
    private SceneChange _sceneChange;

	void Start () {
        _sceneChange = GameObject.FindGameObjectWithTag("Player").GetComponent<SceneChange>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _sceneChange.ChangeScene(SceneName);
        }
    }
}
