using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SimpleWorldGenerator : MonoBehaviour
{
    public GameObject prefab;
    public float chance;

    public static Vector3 worldSize;

    private int _worldHeight;
    private Terrain _terrain;

    private void Awake()
    {
        _terrain = GetComponentInChildren<Terrain>();
        worldSize = new Vector3(_terrain.terrainData.size.x, _worldHeight, _terrain.terrainData.size.z);
    }

    private void Start()
    {
        for (int x = 0; x < worldSize.x; x++)
        {
            for (int z = 0; z < worldSize.z; z++)
            {

                Vector3 position = new Vector3(x, 0, z) + _terrain.transform.position;
                Vector3Int coords = GridNavigation.PositionToCoords(position);
                float r = Random.Range(0f, 100f);

                if (r <= chance)
                {
                    GameObject obj = Instantiate(prefab, GridNavigation.GetCellCenterPosition(coords), Quaternion.identity, transform);
                    GridManager.OcupateCell(coords, obj);
                }

                if (x == worldSize.x / 2 + 1 && z == worldSize.z / 2 + 1)
                {
                    ObjectPool.CreateLootBox(GridNavigation.GetCellCenterPosition(coords), ObjectPool.GetRandomItem());
                    //GameObject obj = Instantiate(prefab, GridNavigation.GetCellCenterPosition(coords), Quaternion.identity, transform);
                    //GridManager.OcupateCell(coords, obj);
                }
            }
        }
    }
}
