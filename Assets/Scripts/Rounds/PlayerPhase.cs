using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhase : MonoBehaviour
{
    public EnemyPhase enemy;
    public bool playerPhase = false;

    // Given time for player phase (seconds)
    public float playerPhaseTime = 180.0f;
    public float timeRemain = 0f;
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPhase)
        {
            timeRemain = (timer - Time.time);
            if(Time.time > timer)
            {
                EndPhase();
            }
        }
    }

    public void StartPhase()
    {
        playerPhase = true;
        timer = Time.time + playerPhaseTime;
    }

    public void EndPhase()
    {
        playerPhase= false;
        timeRemain= 0f;
        timer= 0f;
        enemy.StartPhase();
    }
}
