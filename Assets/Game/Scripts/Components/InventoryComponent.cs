using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InventoryComponent : BaseComponent
{
    public InventoryConfig inventoryConfig;
    //public List<InventorySlotData> slotsData = new List<InventorySlotData>();

    public ItemsStorage itemsStorage;

    public UnityEvent InventoryChanged = new UnityEvent();

    protected override void InitComponent(BaseConfig config)
    {
        inventoryConfig = config as InventoryConfig;

        itemsStorage = transform.AddComponent<ItemsStorage>();
        itemsStorage.InitStorage(inventoryConfig.inventorySize);
        //for (int i = 0; i < inventoryConfig.inventorySize; i++)
        //{
        //    InventorySlotData slotData = new InventorySlotData();
        //    slotData.slotType = SlotType.Cargo;
        //    slotsData.Add(slotData);
        //}
    }

    //public bool AddItem(ItemData itemData)
    //{
    //    for (int i = 0;i < inventoryConfig.inventorySize; i++)
    //    {
    //        if (slotsData[i].itemData == null)
    //        {
    //            slotsData[i].itemData = itemData;


    //            onInventoryChanged.Invoke();
    //            return true;
    //        }
    //    }

    //    return false;
    //}
}
