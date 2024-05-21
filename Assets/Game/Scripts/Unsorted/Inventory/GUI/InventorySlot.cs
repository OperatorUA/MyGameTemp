using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private Inventory _inventoryUI;
    [SerializeField] private GameObject _inventoryItemUiPrefab;

    public ItemData itemData;
    public int index;
    
    private void Awake()
    {
        _inventoryUI = transform.parent.GetComponent<Inventory>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount != 0) return;

        InventoryItem droppedItem = eventData.pointerDrag.GetComponent<InventoryItem>();

        InventorySlot prevSlot = droppedItem.parentAfterDrag.GetComponent<InventorySlot>();
        prevSlot.itemData = itemData;

        droppedItem.parentAfterDrag = transform;
        itemData = droppedItem.itemData;

        _inventoryUI.unitStorage.ReplaceItems(prevSlot.index, index);
    }

    public void Refresh()
    {
        if (transform.childCount == 0 && itemData != null)
        {
            GameObject item = Instantiate(_inventoryItemUiPrefab, transform);
            InventoryItem inventoryItemUI = item.GetComponent<InventoryItem>();
            inventoryItemUI.UpdateData(itemData);
        }

        else if (transform.childCount > 0 && itemData == null)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}