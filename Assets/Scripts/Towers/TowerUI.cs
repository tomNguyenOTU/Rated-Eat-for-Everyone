using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // not sure if i need this just leaving this here in case i do

public class TowerUI : MonoBehaviour
{
    // placeholder
    // don't use this in the final version
    // merge this into a UIManager or GameManager or something

    public TowerPlacement _towerPlace;
    public Canvas _canvas;
    public List<GameObject> _buttons = new List<GameObject>();
    public List<TowerButton> _towerButtons = new List<TowerButton>();

    private int playerMoney;

    private bool mouseHeld;

    private int buttonDragged = -1; // default value

    private void Start()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _towerButtons.Add(_buttons[i].GetComponent<TowerButton>());
        }
    }

    private void Update()
    {
        mouseHeld = Input.GetMouseButton(0);
        //Debug.Log(mouseHeld);

        for (int i = 0; i < _towerButtons.Count; i++) 
        {
            if (mouseHeld && _towerButtons[i].GetMouseOver())
            {
                buttonDragged = i;
                i = _towerButtons.Count; // end loop
            }
        }

        if (buttonDragged != -1 && !_towerButtons[buttonDragged].GetMouseOver())
        {
            if (mouseHeld)
            {
                _towerPlace.BuildMode(true);
                _towerPlace.UpdateHoverTile();
                _towerPlace.ProjectTowerForBuilding(_towerPlace.GetTower());
            }

            if (Input.GetMouseButtonUp(0))
            {
                _towerPlace.BuildTower(_towerPlace.GetTower());
                _towerPlace.BuildMode(false);

                buttonDragged = -1;
            }
        }

    }



    // click and drag:
    // player clicks button
    // player moves mouse off UI, instantiate projection to cursor
    // continue to project
    // build tower when player releases 
}
