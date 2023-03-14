using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField] EnemyPhase enemy;
    [SerializeField] Camera cam;
    [SerializeField] GameObject spawnCenter;
    [SerializeField] float spawnDist;

    public bool routineRun = false;

    public Dictionary<int, Dictionary<int, getWaveInfo.Enemy>> waveInfo;

    private void Start()
    {
        
    }

    public IEnumerator spawnWave(Dictionary<int, getWaveInfo.Enemy> waveDict, float secondsDelay)
    {
        routineRun = true;
        
        int objsInitialized = 0;
        int totalObjs = 0;

        for (int i = 0; i < waveDict.Count; i++)
        {
            totalObjs += waveDict[i].quantity;
        }

        while (objsInitialized < totalObjs)
        {
            for (int j = 0; j < waveDict.Count; j++)
            {
                for (int k = 0; k < waveDict[j].quantity; k++)
                {
                    Vector3 loc = GetSpawnLocation(spawnCenter.transform, spawnDist);

                    Instantiate(waveDict[j].type, loc, Quaternion.identity);

                    objsInitialized++;
                    yield return new WaitForSeconds(secondsDelay);
                }
            }
        }

        routineRun = false;
    }

    private Vector3 GetSpawnLocation(Transform centerTransform, float spawnDistance)
    {
        float dir = Random.Range(0f, 2f * Mathf.PI);
        Vector3 spawnDirection = new Vector3 (Mathf.Cos(dir), Mathf.Sin(dir), 0f);
        Vector3 spawnLocation = centerTransform.position + (spawnDirection * spawnDistance);
        return spawnLocation;
    }
}
