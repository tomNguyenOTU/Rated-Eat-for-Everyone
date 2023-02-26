using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class TowerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouseOver;
    private Button _button;

    public GameObject _towerPrefab;

    private void Start()
    {
        _button = GetComponent<Button>();
    }

    // there's a event called "OnMouseDrag" but i'll be honest i'm too lazy to read docs rn, if this works it works
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }

    public bool GetMouseOver()
    {
        return mouseOver;
    }
}
