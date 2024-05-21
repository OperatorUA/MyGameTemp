using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class ItemsStorage : MonoBehaviour
{
    public int maxSize = 0;
    [SerializeField] protected List<ItemData> _items = new List<ItemData>();
    public UnityEvent StorageChanged = new UnityEvent();

    public void InitStorage(int maxSize)
    {
        this.maxSize = maxSize;
        for (int i = 0; i < maxSize; i++)
        {
            _items.Add(null);
        }
        StorageChanged.AddListener(OnStorageChanged);
    }

    protected virtual void OnStorageChanged()
    {

    }

    public bool PutItem(ItemData itemData)
    {
        if (itemData == null) return false;

        int sameItemSlotIndex = _items.IndexOf(itemData);

        if (sameItemSlotIndex != -1)
        {
            _items[sameItemSlotIndex].count += itemData.count;
            StorageChanged?.Invoke();
            return true;
        }

        int freeSlotIndex = GetFreeSlot();
        if (freeSlotIndex != -1)
        {
            _items[freeSlotIndex] = itemData;
            StorageChanged?.Invoke();
            return true;
        }

        return false;
    }

    public bool SendFirstItem(ItemsStorage reciver)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i] != null && reciver.PutItem(_items[i]))
            {
                RemoveItem(i);
                return true;
            }
        }

        return false;
    }

    public void SendAllItems(ItemsStorage reciver)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (SendFirstItem(reciver) == false) break;
        }
    }

    public ItemData GetItem(int index)
    {
        if (index < _items.Count)
        {
            return _items[index];
        }

        return null;
    }

    public void ReplaceItems(int index1, int index2)
    {
        ItemData temp = _items[index1];

        _items[index1] = _items[index2];
        _items[index2] = temp;
    }

    public void DropItem(ItemData item)
    {
        int itemIndex = _items.IndexOf(item);

        if (RemoveItem(itemIndex))
        {
            List<ItemData> loot = new List<ItemData>();
            loot.Add(item);
            ObjectPool.CreateLootBox(transform.position, loot);
        }
    }

    public void DropAllItems()
    {
        ObjectPool.CreateLootBox(transform.position, _items);
        RemoveAllItems();
    }

    private bool RemoveItem(int index)
    {
        if (index >= 0 && index < _items.Count)
        {
            _items[index] = null;
            StorageChanged?.Invoke();
            return true;
        }
        return false;
    }

    protected void RemoveAllItems()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            _items[i] = null;
        }
        StorageChanged?.Invoke();
    }

    protected int GetFreeSlot()
    {
        if (maxSize == 0) // 0 = infinity
        {
            _items.Add(null);
            return _items.Count - 1;
        }

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i] == null) return i;
        }

        return -1;
    }

    protected bool isEmpty()
    {
        bool result = true;
        if (_items.Count == 0) return true;
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i] != null) result = false;
        }
        
        return result;
    }
}
