using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public GameObject towerPrefab;
    public GameObject towerProjection;
    public LayerMask layerMask;

    private GameObject hoveredTile;

    public Camera _cam;
    public MapGenerator _map;

    private bool building;

    private Transform _trans;

    private void Start()
    {
        BuildMode(true);
    }

    private void Update()
    {
        if (building)
        {
            if (towerProjection != null)
            {
                UpdateHoverTile();
                
                // gridsnap
                if (hoveredTile != null)
                {
                    towerProjection.transform.position = hoveredTile.transform.position;
                }
            }
        }
    }

    public Vector2 GetMousePosition()
    {
        return _cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void UpdateHoverTile()
    {
        Vector2 mousePosition = GetMousePosition();

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 0.1f, layerMask, -100, 100);

        // no hovered tile
        if (hit.collider == null)
            return;

        if (_map.GetMapTiles().Contains(hit.collider.gameObject))
        {
            hoveredTile = hit.collider.gameObject;
        }
    }

    public void BuildMode(bool building) // do not run in Update() kthanks
    {
        this.building = building;

        // end if turning off the build mode
        if (!building)
            return;

        towerProjection = Instantiate(towerPrefab, _trans);

        // i think this removes a component
        if (towerProjection.GetComponent<Tower>() != null)
        {
            Destroy(towerProjection.GetComponent<Tower>());
        }
    }
}
