using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Use this for initialization
    void Start () {

        Invoke("T1", 1);
        Invoke("T2", 5);
        Invoke("T3", 10);
        Invoke("T4", 15);
        Invoke("T5", 20);
        Invoke("T6", 25);
        Invoke("T7", 30);
        Invoke("T8", 35);
        Invoke("T9", 40);
        Invoke("T10", 45);

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

    void T8()
    {

        text8.SetActive(true);

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
}
