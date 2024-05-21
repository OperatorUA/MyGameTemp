using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HarvestComponent : BaseComponent
{
    public HarvestConfig harvestConfig;

    public float harvestProgress = 0f;

    protected override void InitComponent(BaseConfig config)
    {
        harvestConfig = config as HarvestConfig;
    }

    public void Harvesting(int skillLvl, ExpComponent expComponent)
    {
        float harvestSpeed = Mathf.Pow(GamePrefs.levelSpeedMultiplier, skillLvl) * Time.deltaTime;

        harvestProgress += harvestSpeed;
        expComponent.AddExp(harvestConfig.skillType, harvestSpeed);
        Debug.Log("Harvest in progress...");
    }

    public void DropLoot()
    {
        foreach (DropItem dropItem in harvestConfig.dropList)
        {
            float r = Random.Range(0, 101);
            if (r <= dropItem.chanse)
            {
                dropItem.ItemData.count = Random.Range(dropItem.minCount, dropItem.maxCount + 1);

                GameObject lootBox = Instantiate(dropItem.ItemData.prefab, transform.position, Quaternion.identity);
                GridManager.OcupateCell(GridNavigation.PositionToCoords(transform.position), lootBox);

                ItemsStorage lootBoxComponent = lootBox.GetComponent<ItemsStorage>();
                lootBoxComponent.PutItem(dropItem.ItemData);
                //inventoryComponent.AddItem(dropItem.ItemData);
            }
        }

        Debug.Log("Items dropped!");
        Destroy(gameObject);


    }
}
