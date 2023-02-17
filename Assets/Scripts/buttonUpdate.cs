using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonUpdate : MonoBehaviour
{
    PlayerPhase player;

    // Update is called once per frame
    void Update()
    {
        if (!player.playerPhase)
        {
            this.enabled = false;
        }
        else if(player.playerPhase && this.enabled == false) 
        {
            this.enabled = true;
        }
    }
}
