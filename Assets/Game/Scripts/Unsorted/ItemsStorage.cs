using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsStorage : MonoBehaviour
{
    [SerializeField] private List<ItemData> items;

    public void PutItem(ItemData itemData)
    {
        items.Add(itemData);
    }

    public void GetAll(InventoryComponent inventoryComponent)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (GetFirstItem(inventoryComponent))//temp
            {
                OnGetItem();
            } else
            {
                break;
            }
        }
    }
    public bool GetFirstItem(InventoryComponent inventoryComponent)
    {
        if (inventoryComponent.AddItem(items.First()))
        {
            items.Remove(items.First());
            OnGetItem();
            return true;//temp
        }

        return false;//temp
    }

    public bool HasItem(ItemData itemData)
    {
        return items.Contains(itemData);
    }

    private void OnGetItem()
    {
        if (items.Count == 0)
        {
            Destroy(gameObject);
        }
    }
}
