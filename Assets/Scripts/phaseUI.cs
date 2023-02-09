using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class phaseUI : MonoBehaviour
{
    TextMeshProUGUI m_TextMeshProUGUI;
    public PlayerPhase player;
    public EnemyPhase enemy;

    // Start is called before the first frame update
    void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.playerPhase)
        {
            m_TextMeshProUGUI.text = $"Phase: Player Phase\nTime: {Mathf.Floor(player.phaseTimer)}";
        }
        if (enemy.enemyPhase)
        {
            m_TextMeshProUGUI.text = $"Phase: Enemy Phase\nEnemies: {enemy.enemyCount}";
        }
    }

    public void ButtonClick()
    {
        if (player.playerPhase)
        {
            player.EndPhase();
        }
        else if (enemy.enemyPhase)
        {
            enemy.KillPhase();
        }
    }
}
