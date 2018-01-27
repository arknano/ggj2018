using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFillSetter : MonoBehaviour {

    public float UpdateTimer;
    public FloatVariable FloatVariable;
    private float _updateTime;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    void Update () {
        _updateTime += Time.deltaTime;
        if (_updateTime > UpdateTimer)
        {
            _updateTime = 0;
            _image.fillAmount = FloatVariable.Value / 100;
        }
	}
}
