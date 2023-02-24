using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonUpdate : MonoBehaviour
{
    PlayerPhase player;

    // Update is called once per frame
    void Update()
    {
        /* i think this is code for preventing building during defend so i'm commenting this out for the time being
        
        if (!player.playerPhase)
        {
            this.enabled = false;
        }
        else if (player.playerPhase && this.enabled == false) 
        {
            this.enabled = true;
        }

        */
    }
}
