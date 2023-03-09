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
        /* Get information from all "Waves" objects.
         * 
         * Information is saved in a Dictionary with the format: [Wave Number, Wave Dictionary],
         * Where [Wave Dictionary] has information on each individual enemy type:
         * 
         *      [Wave Dictionary] = [Enemy Index, Enemy Info]
         *      
         *      Such that:
         *      
         *          [Enemy Info] = 
         *          {
         *              GameObject type;
         *              int quantity;
         *          }
         *          
         *      Enemy Info is a struct with variables for an enemies type - which prefab will be used to spawn,
         *      And quantity - how many enemies of this type are spawned.
         *      
         *      Enemies are spawned in the order they appear.
         *      Once all enemies of one type are spawned, 
         *      The next group is automatically spawned until all types have been spawned as many times as indicated in [quantity]
         *      
         *      EX: Type A, Quantity 10,
         *          Type B, Quantity 5
         *          
         *          First, 10 objects of Type A are spawned,
         *          Then, 5 objects of Type B are spawned.
         */

        GameObject[] waves = GameObject.FindGameObjectsWithTag("Waves");
        for (int waveIndex = 0; waveIndex < waves.Length; waveIndex++)
        {
            Dictionary<int, getWaveInfo.Enemy> wave = GameObject.Find("Wave" + (waveIndex + 1)).GetComponent<getWaveInfo>().keyValuePairs;
            waveInfo.Add(waveIndex, wave);
        }
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
