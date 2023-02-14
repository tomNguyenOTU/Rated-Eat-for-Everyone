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

    private Tower _tower;

    private void Start()
    {
        BuildMode(true);

        _tower = towerPrefab.GetComponent<Tower>();
    }

    private void Update()
    {
        UpdateHoverTile();

        ProjectTowerForBuilding(_tower);
    }

    public Vector2 GetMousePosition()
    {
        return _cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public void ProjectTowerForBuilding(Tower tower)
    {
        // yes i know shut up this is easier for me to read

        // checks if the projection needs to be updated
        if (!building)
            return;

        if (towerProjection == null)
            return;

        if (hoveredTile == null)
            return;

        // pseudocode stuff:
        // find the coordinates of the hovered tile
        // add the width of the tower (a, a) to the coordinates (x, y)
        // check if the new coordinates are outside of the grid dimensions (i, j)

        // check if the projection would be out of bounds
        int tileIndex = _map.GetMapTiles().IndexOf(hoveredTile);
        Vector3 tileCoordinates = _map.GetMapTileCoordinates()[tileIndex];
        Debug.Log(tileCoordinates);

        tileCoordinates += new Vector3(tower.GetWidth(), tower.GetWidth(), 0);

        Vector3 gridDimensions = new Vector3(_map.GetMapWidth(), _map.GetMapHeight(), 0);

        if (tileCoordinates.x > gridDimensions.x || tileCoordinates.y > gridDimensions.y)
            return;

        // update the projection
        towerProjection.transform.position = hoveredTile.transform.position + ProjectionOffset(tower);
    }

    public void UpdateHoverTile()
    {
        RaycastHit2D hit = Physics2D.Raycast(GetMousePosition(), Vector2.zero, 0.1f, layerMask, -100f, 100f);

        // no hovered tiles
        if (hit.collider == null)
        {
            Debug.Log("no collider found at mouse position"); 
            return;
        }

        if (_map.GetMapTiles().Contains(hit.collider.gameObject))
        {
            hoveredTile = hit.collider.gameObject;
        }
    }

    public Vector3 ProjectionOffset(Tower tower)
    {
        float baseOffset = _map.GetTileWidth();
        int towerWidth = _tower.GetWidth();

        float adjustedOffset;

        if (towerWidth <= 1)
        {
            return Vector3.zero;
        }
        else
        {
            adjustedOffset = baseOffset / (float)towerWidth;
        }

        Vector3 offset = new Vector3(adjustedOffset, adjustedOffset);
        Debug.Log($"{baseOffset} / {towerWidth} = {adjustedOffset}");

        return offset;
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
