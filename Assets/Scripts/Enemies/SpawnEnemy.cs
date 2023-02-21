using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemyPrefab;

    public void Spawn(GameObject template, Vector3 spawnPosition)
    {
        Instantiate(template, spawnPosition, Quaternion.identity);
    }
}
