using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

        // commented out so i can send this to a manager script 

        //if (building)
        //{
        //    UpdateHoverTile();
        //    ProjectTowerForBuilding(_tower);

        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        if (hoveredTile != null)
        //        {
        //            BuildTower(_tower);
        //        }

        //        BuildMode(false);
        //    }

        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        BuildMode(false);
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    // PLEASE DON'T BREAK PLEASE DON'T BREAK PLEASE DON'T BREAK PLEASE
        //    if (building)
        //        BuildMode(false);
        //    else
        //        BuildMode(true);
        //}
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
        // Debug.Log(tileCoordinates);

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
            hoveredTile = null;
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
        if (this.building == building)
            return;

        this.building = building;

        if (building)
        {
            towerProjection = Instantiate(towerPrefab, new Vector3(-40, 40, 0), Quaternion.identity, _trans);

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

    public bool CheckForTower(Tower tower)
    {
        float tileWidth = _map.GetTileWidth();
        int towerWidth = tower.GetWidth();

        float baseOffset = tileWidth * (towerWidth - 1);

        Vector2[] offsets = new Vector2[4];
        RaycastHit2D[] hit = new RaycastHit2D[4];

        for (int i = 0; i < 2; i++)
        { 
            for (int j = 0; j < 2; j++)
            {
                // binary trick; there's probably a better solution but i like this one
                offsets[i*2 + j] = new Vector2(baseOffset * i, baseOffset * j);
                hit[i*2 + j] = RaycastMouseToMask(towerMask, offsets[i*2 + j]);
            }
        }

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider != null)
            {
                Debug.Log("tower in projection location");
                return true;
            }
        }

        Debug.Log("no tower in projection location");
        return false;


        // alternate solution involving colliders
        //BoxCollider2D projectionHitbox = towerProjection.GetComponent<BoxCollider2D>();
        //bool projectionTouchingTower = projectionHitbox.IsTouchingLayers(towerMask);

        //if (projectionTouchingTower)
        //{
        //    Debug.Log("tower in projection location");
        //    return true;
        //}
        //else
        //{
        //    Debug.Log("no tower in projection location");
        //    return false;
        //}
    }

    public void BuildTower(Tower tower)
    {
        // if in build mode, hovering over a tile, and there is no tower
        if (!building)
            return;

        if (hoveredTile == null)
            return;

        if (CheckForTower(tower))
            return;

        // build the tower
        GameObject newTowerObject = Instantiate(towerPrefab, towerProjection.transform.position, Quaternion.identity, _trans);
        newTowerObject.layer = 6;

        // destroy the projection
        // BuildMode(false);
    }
    
    public bool GetBuildMode()
    {
        return building;
    }

    public Tower GetTower()
    {
        return _tower;
    }
}
