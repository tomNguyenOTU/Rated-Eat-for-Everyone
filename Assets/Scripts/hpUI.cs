using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class hpUI : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    public TowerStats tower;

    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI= GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMeshProUGUI.text = "Core HP: " + tower.hitPoints;
    }
}
