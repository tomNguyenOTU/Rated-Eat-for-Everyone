using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhase : MonoBehaviour
{
    public bool playerPhase = false;

    // Given time for player phase (seconds)
    public float playerPhaseTime = 180.0f;

    // Current time remaining
    float phaseTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPhase()
    {
        playerPhase = true;
        phaseTimer = playerPhaseTime;
    }

    public void EndPhase()
    {
        phaseTimer = 0;
        playerPhase= false;
    }
}
