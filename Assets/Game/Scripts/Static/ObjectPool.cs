using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject lootBoxPrefab;
    private static List<ItemData> itemsData;

    public static ObjectPool _instance { get; private set; }
    public static ObjectPool Instance
    {
        get
        {
            if (_instance == null) Init();
            return _instance;
        }
    }

    private void Awake()
    {
        Init();
    }

    private static void Init()
    {
        _instance = FindObjectOfType<ObjectPool>();
        itemsData = ResourceLoader.LoadAll<ItemData>("Items");
    }

    public static ItemData GetRandomItem()
    {
        int r = Random.Range(0, itemsData.Count);
        return itemsData[r];
    }

    public static ItemData GetItemById(string id)
    {
        ItemData result = null;
        foreach (ItemData item in itemsData)
        {
            if (item.id == id)
            {
                result = item;
                break;
            }
        }
        if (result == null) Debug.LogWarning($"Item with id:{id} was not found!");
        return result;
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
    public static void CreateLootBox(Vector3 position, ItemData item) //temp
    {
        GameObject lootBox = Instantiate(Instance.lootBoxPrefab, GridNavigation.GetCellCenterPosition(position), Quaternion.identity);
        GridManager.OcupateCell(GridNavigation.PositionToCoords(position), lootBox);
        ItemsStorage itemsStorage = lootBox.GetComponent<ItemsStorage>();
        itemsStorage.PutItem(item);
    }
}
