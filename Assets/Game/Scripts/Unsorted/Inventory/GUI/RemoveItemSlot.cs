using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RemoveItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        InventoryItemUI droppedItem = droppedObject.GetComponent<InventoryItemUI>();

        InventorySlotUI prevSlot = droppedItem.parentAfterDrag.GetComponent<InventorySlotUI>();
        prevSlot.slotData.itemData = null;
        prevSlot.Refresh();

        Destroy(droppedItem.gameObject);
    }
}
