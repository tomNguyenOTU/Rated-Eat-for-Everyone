using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    public GameObject towerPrefab;
    public GameObject towerProjection;
    public LayerMask gridMask;
    public LayerMask towerMask;

    private GameObject hoveredTile;

    public Camera _cam;
    public MapGenerator _map;

    private bool building;

    private Transform _trans;

    private Tower _tower;

    private void Start()
    {
        _tower = towerPrefab.GetComponent<Tower>();
    }

    private void Update()
    {
        // placeholder testing stuffs i dunno
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // PLEASE DON'T BREAK PLEASE DON'T BREAK PLEASE DON'T BREAK PLEASE
            if (building)
                BuildMode(false);
            else
                BuildMode(true);
        }

        if (building)
        {
            UpdateHoverTile();
            ProjectTowerForBuilding(_tower);

            if (Input.GetMouseButton(0))
            {
                BuildTower();
            }
        }
    }

    public Vector2 GetMousePosition()
    {
        return _cam.ScreenToWorldPoint(Input.mousePosition);
    }

    public RaycastHit2D RaycastMouseToMask(LayerMask mask)
    {
        return Physics2D.Raycast(GetMousePosition(), Vector2.zero, 0.1f, mask, -100f, 100f);
    }

    public RaycastHit2D RaycastMouseToMask(LayerMask mask, Vector2 offset)
    {
        return Physics2D.Raycast(GetMousePosition() + offset, Vector2.zero, 0.1f, mask, -100f, 100f);
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
        RaycastHit2D hit = RaycastMouseToMask(gridMask);

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

        if (building)
        {
            towerProjection = Instantiate(towerPrefab, _trans);

            // makes the projection not a real tower
            if (towerProjection.GetComponent<Tower>() != null)
            {
                Destroy(towerProjection.GetComponent<Tower>());
            }
        }
        else 
        {
            Destroy(towerProjection);
            towerProjection = null;
        }
    }

    public bool CheckForTower()
    {
        //Vector2[] offsets = new Vector2[4];
        //RaycastHit2D[] hit = new RaycastHit2D[4];

        //for (int i = 0; i < 2; i++)
        //{

        //}

        BoxCollider2D projectionHitbox = towerProjection.GetComponent<BoxCollider2D>();
        bool projectionTouchingTower = projectionHitbox.IsTouchingLayers(towerMask);

        // RaycastHit2D hit = RaycastMouseToMask(towerMask, ProjectionOffset(_tower));

        //    if (hit.collider == null)
        //        return false;
        //    else
        //    {
        //        Debug.Log("tower in projection location");
        //        return true;
        //    }

        if (projectionTouchingTower)
        {
            Debug.Log("tower in projection location");
            return true;
        }
        else
            return false;
    }

    public void BuildTower()
    {
        // if in build mode, hovering over a tile, and there is no tower
        if (!building)
            return;

        if (hoveredTile == null)
            return;

        if (CheckForTower())
            return;

        // build the tower
        GameObject newTowerObject = Instantiate(towerPrefab, towerProjection.transform.position, Quaternion.identity, _trans);
        newTowerObject.layer = 6;

        // run this if the tower is actually built; destroys the projection
        BuildMode(false);
    }
}
