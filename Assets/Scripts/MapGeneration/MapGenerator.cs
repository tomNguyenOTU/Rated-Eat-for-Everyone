using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject mapTile;

    public int mapWidth;
    public int mapHeight;
    public float tileWidth;
    public float mapOffsetX;
    public float mapOffsetY;

    private Transform _trans;
    private List<GameObject> mapTiles = new List<GameObject>();

    private void Start()
    {
        _trans = GetComponent<Transform>();

        CreateMap();
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
                Vector3 maptilePosition = new Vector3(x, y, 0);

                GameObject newTile = Instantiate(mapTile, maptilePosition, Quaternion.identity, _trans);
                mapTiles.Add(newTile);
            }
        }
    }

    public List<GameObject> GetMapTiles()
    {
        return mapTiles;
    }
}
