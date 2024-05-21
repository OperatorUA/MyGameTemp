using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Events;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryItemPrefab;
    public List<InventorySlotData> unitSlotsData;

    public void Refresh()
    {
        unitSlotsData = GUIManager.Instance.playerControls.selectedUnit.inventoryComponent.slotsData;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform slotTransform = transform.GetChild(i);
            InventorySlotUI inventorySlotUI = slotTransform.GetComponent<InventorySlotUI>();

            if (i < unitSlotsData.Count)
            {
                inventorySlotUI.slotData = unitSlotsData[i];
            } else
            {
                inventorySlotUI.slotData = null;
            }
            
            inventorySlotUI.Refresh();
        }
    }
}
