﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenToPosition : MonoBehaviour {

    public Transform StartingLocation;
    public Transform TweenToTransform;
    public float TweenTime;

	void OnEnable () {
        if (LeanTween.isTweening(gameObject))
        {
            LeanTween.cancel(gameObject);
        }

        transform.position = StartingLocation.position;
        LeanTween.move(gameObject, TweenToTransform.position, TweenTime).setEase(LeanTweenType.easeInOutQuad);
	}
	
}
