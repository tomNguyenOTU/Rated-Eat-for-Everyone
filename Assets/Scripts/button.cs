using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    public EnemyPhase phase;
    public void ButtonPress()
    {
        phase.StartPhase();
    }
}
