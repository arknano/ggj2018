﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCounter : MonoBehaviour
{
    public GameObject Door;


    private List<GameObject> enemies = new List<GameObject>();
    private Text remainderText;

    void Start()
    {
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        remainderText = GetComponentInChildren<Text>();
        setRemainingCount(enemies.Count);
    }

    public void EnemyKilled(GameObject gameObject)
    {
        if (enemies.Contains(gameObject))
        {
            enemies.Remove(gameObject);
            setRemainingCount(enemies.Count);
        }

        if (enemies.Count == 0)
        {

            Door.SetActive(true);
        }
    }

    private void setRemainingCount(int count)
    {
        remainderText.text = "Enemies Remaining: " + enemies.Count;
    }
}
