using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLOW_CONTROLLER : MonoBehaviour
{
    PlayerPhase player_phase;
    EnemyPhase enemy_phase;
    Waves waves;

    public Dictionary<int, getWaveInfo.Enemy> currentWaveInfo;

    // Track Win State
    int maxWave;
    int currentWave = 0;

    // 0 == Player, 1 == Enemy
    int currentPhase;

    // Start is called before the first frame update
    void Start()
    {
        player_phase = GetComponent<PlayerPhase>();
        enemy_phase = GetComponent<EnemyPhase>();
        waves = GetComponent<Waves>();

        maxWave = waves.waveInfo.Count;

        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWave > maxWave)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        // Placeholder
    }

    void StartGame()
    {
        currentPhase = 0;
        player_phase.StartPhase();
    }

    public void EndPhase()
    {
        if (currentPhase == 0)
        {
            // Switch to Enemy Phase
            currentPhase = 1;
            enemy_phase.StartPhase();
        }
        else
        {
            // Switch to Player Phase
            currentPhase = 0;
            currentWave++;
            player_phase.StartPhase();
        }
    }
}
