using AYellowpaper.SerializedCollections;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryConfig : BaseConfig
{
    public int inventorySize;
}

[System.Serializable]
public class InventorySlotData
{
    public SlotType slotType;
    public ItemData itemData;
}
public enum SlotType
{
    Cargo,
    Hemlet,
    Chest,
    Pants,
    Ring,
}