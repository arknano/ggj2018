using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCounter : MonoBehaviour {

    public GameObject levelPortal;

    private List<GameObject> enemies = new List<GameObject>();
    void Start() {
        enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        levelPortal.SetActive(false);
    }

    public void EnemyKilled(GameObject gameObject)
    {
        if (enemies.Contains(gameObject))
        {
            enemies.Remove(gameObject);
            Debug.Log("There are " + enemies.Count + " enemies");
        }

        if (enemies.Count == 0)
        {
            levelPortal.SetActive(true);
        }
    }
}
