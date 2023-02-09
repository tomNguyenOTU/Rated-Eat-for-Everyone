using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhase : MonoBehaviour
{
    public EnemyPhase enemy;
    public bool playerPhase = false;

    // Given time for player phase (seconds)
    public float playerPhaseTime = 180.0f;

    // Current time remaining
    public float phaseTimer = 0f;
    float currentTime = 0f;
    float lastCheck = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPhase)
        {
            currentTime = Time.time;
            phaseTimer -= (currentTime - lastCheck);
            if (phaseTimer <= 0f)
            {
                EndPhase();
            }
            lastCheck = currentTime;
        }
    }

    public void StartPhase()
    {
        playerPhase = true;
        phaseTimer = playerPhaseTime;
        currentTime = Time.time;
        lastCheck = currentTime;
    }

    public void EndPhase()
    {
        phaseTimer = 0;
        playerPhase= false;
        enemy.StartPhase();
    }
}
