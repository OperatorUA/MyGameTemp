using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryItemPrefab;
    public ItemsStorage unitStorage;

    public void Refresh()
    {
        unitStorage = GUIManager.Instance.playerControls.selectedUnit.inventoryComponent.itemsStorage;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform slotTransform = transform.GetChild(i);
            InventorySlot inventorySlot = slotTransform.GetComponent<InventorySlot>();
            inventorySlot.index = i;

            if (i < unitStorage.maxSize)
            {
                if (!inventorySlot.gameObject.activeSelf) inventorySlot.gameObject.SetActive(true);
                inventorySlot.itemData = unitStorage.GetItem(i);
            } else
            {
                if (inventorySlot.gameObject.activeSelf) inventorySlot.gameObject.SetActive(false);
            }
            
            inventorySlot.Refresh();
        }

        Debug.Log("Inventory refreshed!");
    }
}
