using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject lootBoxPrefab;

    public static ObjectPool _instance { get; private set; }
    public static ObjectPool Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<ObjectPool>();
            return _instance;
        }
    }

    public static void CreateLootBox(Vector3 position, List<ItemData> items)
    {
        GameObject lootBox = Instantiate(Instance.lootBoxPrefab, GridNavigation.GetCellCenterPosition(position), Quaternion.identity);
        GridManager.OcupateCell(GridNavigation.PositionToCoords(position), lootBox);
        ItemsStorage itemsStorage = lootBox.GetComponent<ItemsStorage>();

        foreach (ItemData item in items)
        {
            itemsStorage.PutItem(item);
        }
    }
}
