using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public SpawnEnemy spawner;
    public EnemyPhase enemy;
    public Camera cam;
    public bool routineRun = false;

    private List<GameObject> enemies = new List<GameObject>();

    public IEnumerator spawnWave(string spawnDirection, float spawnCooldown, GameObject enemyType, int enemyCount)
    {
        routineRun = true;
        float rangeX_Min = 0f;
        float rangeY_Min = 0f;
        float rangeX_Max = 0f;
        float rangeY_Max = 0f;

        switch (spawnDirection)
        {
            case "Left":
                rangeX_Min = -150f;
                rangeX_Max = -50f;
                rangeY_Min = 0f;
                rangeY_Max = cam.pixelHeight;
                break;

            case "Right":
                rangeX_Min = cam.pixelWidth + 150f;
                rangeX_Max = cam.pixelWidth + 50f;
                rangeY_Min = 0f;
                rangeY_Max = cam.pixelHeight;
                break;

            case "Up":
                rangeX_Min = cam.pixelWidth;
                rangeX_Max = 0f;
                rangeY_Min = -50f;
                rangeY_Max = -150;
                break;

            default:
                rangeX_Min = cam.pixelWidth;
                rangeX_Max = 0f;
                rangeY_Min = cam.pixelHeight + 50f;
                rangeY_Max = cam.pixelHeight + 150f;
                break;
        }

        while (enemyCount > 0 && enemy.enemyPhase)
        {
            Vector3 spawnPoint = cam.ScreenToWorldPoint(new Vector3(Random.Range(rangeX_Min, rangeX_Max), Random.Range(rangeY_Min, rangeY_Max), -cam.transform.position.z));

            spawner.Spawn(enemyType, spawnPoint);
            enemyCount--;

            yield return new WaitForSeconds(spawnCooldown);
        }
        routineRun = false;
    }

    private void DirectionSpawn(GameObject enemyType, Transform centerTransform, float spawnDistance)
    {
        float dir = Random.Range(0f, 2f * Mathf.PI);
        Vector3 spawnDir = new Vector3 (Mathf.Cos(dir), Mathf.Sin(dir), 0f);

        Vector3 spawnLocation = centerTransform.position + (spawnDir * spawnDistance);

        spawner.Spawn(enemyType, spawnLocation);
    }
}
