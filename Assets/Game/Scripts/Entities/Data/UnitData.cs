using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Entity/Units/Default Unit")]
public class UnitData : EntityData
{
    public HealthConfig healthConfig;
    public ExpConfig expConfig;
    public InventoryConfig inventoryConfig;

    public float moveSpeed;
}
