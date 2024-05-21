using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryComponent : BaseComponent
{
    public InventoryConfig inventoryConfig;
    public List<InventorySlotData> slotsData = new List<InventorySlotData>();

    public UnityEvent onInventoryChanged = new UnityEvent();

    protected override void InitComponent(BaseConfig config)
    {
        inventoryConfig = config as InventoryConfig;

        for (int i = 0; i < inventoryConfig.inventorySize; i++)
        {
            InventorySlotData slotData = new InventorySlotData();
            slotData.slotType = SlotType.Cargo;
            slotsData.Add(slotData);
        }
    }

    public bool AddItem(ItemData itemData)
    {
        for (int i = 0;i < inventoryConfig.inventorySize; i++)
        {
            if (slotsData[i].itemData == null)
            {
                slotsData[i].itemData = itemData;

                onInventoryChanged.Invoke();
                return true;
            }
        }

        return false;
    }
}
