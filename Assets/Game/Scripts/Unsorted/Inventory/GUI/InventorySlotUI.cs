using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class InventorySlotUI : MonoBehaviour, IDropHandler
{
    private InventoryUI _inventoryUI;
    [SerializeField] private GameObject _inventoryItemUiPrefab;

    public InventorySlotData slotData;
    
    private void Awake()
    {
        _inventoryUI = transform.parent.GetComponent<InventoryUI>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount != 0) return;

        GameObject droppedObject = eventData.pointerDrag;
        InventoryItemUI droppedItem = droppedObject.GetComponent<InventoryItemUI>();

        InventorySlotUI prevSlot = droppedItem.parentAfterDrag.GetComponent<InventorySlotUI>();
        prevSlot.slotData.itemData = slotData.itemData;

        droppedItem.parentAfterDrag = transform;
        slotData.itemData = droppedItem.itemData;
    }

    public void Refresh()
    {
        if (slotData == null)
        {
            if (gameObject.activeSelf) gameObject.SetActive(false);
            return;
        }
        else
        {
            if (!gameObject.activeSelf) gameObject.SetActive(true);
        }

        if (transform.childCount == 0 && slotData.itemData != null)
        {
            GameObject item = Instantiate(_inventoryItemUiPrefab, transform);
            InventoryItemUI inventoryItemUI = item.GetComponent<InventoryItemUI>();
            inventoryItemUI.UpdateData(slotData.itemData);
        }

        else if (transform.childCount > 0 && slotData.itemData == null)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}