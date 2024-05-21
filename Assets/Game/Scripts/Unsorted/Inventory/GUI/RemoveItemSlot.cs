using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RemoveItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        InventoryItem droppedItem = droppedObject.GetComponent<InventoryItem>();

        Inventory inventory = GUIManager.Instance.inventory;
        inventory.unitStorage.DropItem(droppedItem.itemData);

        Destroy(droppedItem.gameObject);
    }
}
