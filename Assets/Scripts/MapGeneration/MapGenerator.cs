using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteAlways]
public class MapGenerator : MonoBehaviour
{
    public GameObject mapTile;

    public int mapWidth;
    public int mapHeight;
    public float tileWidth;
    public float mapOffsetX;
    public float mapOffsetY;

    private int currentMapWidth;
    private int currentMapHeight;
    private float currentTileWidth;
    private float currentMapOffsetX;
    private float currentMapOffsetY;

    private Transform _trans;
    private List<GameObject> mapTiles = new List<GameObject>();
    private List<Vector3> mapTileCoordinates = new List<Vector3>();

    private void Start()
    {
        _trans = GetComponent<Transform>();

        CreateMap();
    }

    private void Update()
    {
        if (mapWidth != currentMapWidth ||
            mapHeight != currentMapHeight ||
            tileWidth != currentTileWidth ||
            mapOffsetX != currentMapOffsetX ||
            mapOffsetY != currentMapOffsetY)
        {
            for(int i = 0; i < _trans.childCount; i++)
            {
                DestroyImmediate(_trans.GetChild(i).gameObject);
            }
            mapTiles.Clear();
            mapTileCoordinates.Clear();
        }

        if (mapTiles.Count == 0)
        {
            CreateMap();
        }
    }

    private void CreateMap()
    {
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                // this could be condensed into one line but readability
                float x = mapOffsetX + i * tileWidth;
                float y = mapOffsetY + j * tileWidth;
                Vector3 mapTilePosition = new Vector3(x, y, 0);

                // this can be expanded into two lines by just setting the position in another line but idk lmao
                GameObject newTile = Instantiate(mapTile, mapTilePosition, Quaternion.identity, _trans);
                newTile.transform.localScale = new Vector3(tileWidth, tileWidth, tileWidth);
                mapTiles.Add(newTile);

                // set the coordinates of the tile
                Vector3 mapTileCoordinates = new Vector3 (i, j, 0);
                this.mapTileCoordinates.Add(mapTileCoordinates);
            }
        }
    }

    public List<GameObject> GetMapTiles()
    {
        return mapTiles;
    }

    public List<Vector3> GetMapTileCoordinates()
    {
        return mapTileCoordinates;
    }

    public float GetTileWidth()
    {
        return tileWidth;
    }

    public Vector2 GetMapCoordinates()
    {
        return new Vector2(mapWidth, mapHeight);
    }

    public int GetMapWidth()
    {
        return mapWidth;
    }

    public int GetMapHeight()
    {
        return mapHeight;
    }
}
