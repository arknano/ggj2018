﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroControl : MonoBehaviour {

    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    public GameObject text5;
    public GameObject text6;
    public GameObject text7;
    public GameObject text8;
    public GameObject text9;
    public GameObject text10;
    public GameObject text11;
    public GameObject text12;

    // Use this for initialization
    void Start () {

        Invoke("T1", 1);
        Invoke("T2", 3);
        Invoke("T3", 7);
        Invoke("T4", 10);
        Invoke("T5", 13);
        Invoke("T6", 16);
        Invoke("T9", 22);
        Invoke("T10", 23);
        Invoke("T11", 24);
        Invoke("T13", 28);
    }
	
	// Update is called once per frame
	void T1 () {

        text1.SetActive(true);
		
	}

    void T2()
    {

        text2.SetActive(true);

    }

    void T3()
    {

        text3.SetActive(true);

    }

    void T4()
    {

        text4.SetActive(true);

    }

    void T5()
    {

        text5.SetActive(true);

    }

    void T6()
    {

        text6.SetActive(true);

    }

    void T7()
    {

        text7.SetActive(true);

    }



    void T9()
    {

        text9.SetActive(true);

    }

    void T10()
    {

        text10.SetActive(true);

    }

    void T11()
    {

        text11.SetActive(true);

    }

    void T12()
    {

        text12.SetActive(true);

    }

    void T13()
    {
        GetComponent<SceneChange>().ChangeScene("D_Test");
    }
}
