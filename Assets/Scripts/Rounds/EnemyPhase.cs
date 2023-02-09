using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhase : MonoBehaviour
{
    public Waves waves;
    public GameObject prefab;

    public bool enemyPhase = false;
    bool activateSpawn = false;

    // Num of enemies spawned during wave (total)
    public int enemyCount;

    // Remaining active enemies
    int liveEnemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyPhase && !waves.routineRun && !activateSpawn)
        {
            StartCoroutine(waves.spawnWave("", 5.0f, prefab, enemyCount));
            activateSpawn = true;
        }

        liveEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (liveEnemyCount == 0 && !waves.routineRun)
        {
            activateSpawn = false;
            EndPhase();
        }
    }

    public void StartPhase()
    {
        enemyPhase = true;
    }

    public void StartPhase(int waveLeng)
    {
        enemyCount = waveLeng;
        enemyPhase = true;
    }

    public void EndPhase()
    {
        enemyPhase = false;
        liveEnemyCount = 0;
    }

    public void KillPhase()
    {
        // Force the current phase to end
        // Removes all active enemies from field

        StopCoroutine("spawnWave");
        EndPhase();
    }
}
