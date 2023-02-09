using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    PlayerPhase player;
    // Start is called before the first frame update
    void Start()
    {
        player= GetComponent<PlayerPhase>();
        player.StartPhase();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
