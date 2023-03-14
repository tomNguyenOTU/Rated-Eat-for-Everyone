using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhase : MonoBehaviour
{
    FLOW_CONTROLLER flow;

    [SerializeField] Waves waves;
    [SerializeField] GameObject prefab;

    [SerializeField] float spawnDelay = 3.0f;

    public bool enemyPhase = false;
    bool activateSpawn = false;

    // Remaining active enemies
    int liveEnemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        flow = GetComponent<FLOW_CONTROLLER>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyPhase && !waves.routineRun && !activateSpawn)
        {
            StartCoroutine(waves.spawnWave(waves.waveInfo[flow.currentWave], spawnDelay));
            activateSpawn = true;
        }

        liveEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if ( enemyPhase && liveEnemyCount == 0 && !waves.routineRun)
        {
            activateSpawn = false;
            EndPhase();
        }
    }

    public void StartPhase()
    {
        enemyPhase = true;
    }

    public void EndPhase()
    {
        enemyPhase = false;
        liveEnemyCount = 0;
        flow.EndPhase();
    }

    public void KillPhase()
    {
        // Force the current phase to end
        // Removes all active enemies from field

        StopCoroutine("spawnWave");
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in Enemies)
        {
            Destroy(enemy);
        }
        EndPhase();
    }
}
