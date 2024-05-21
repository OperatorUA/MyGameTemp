using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class OrderHarvest : BaseOrder
{
    private HarvestComponent harvestComponent;
    private BaseUnit unitComponent;
    private int unitSkillLvl;
    public OrderHarvest(HarvestComponent harvestComponent, BaseUnit unitComponent)
    {
        this.harvestComponent = harvestComponent;
        this.unitComponent = unitComponent;
        unitSkillLvl = unitSkillLvl = unitComponent.expComponent.expConfig.skills[harvestComponent.harvestConfig.skillType].level;
    }
    
    public override void Execute()
    {
        if (IsOrderCompleted())
        {
            OnOrderCompleted.Invoke();
            return;//temp
        }

        harvestComponent.Harvesting(unitSkillLvl, unitComponent.expComponent);
    }

    private bool IsOrderCompleted()
    {
        if (harvestComponent == null) return true;

        if (unitSkillLvl < harvestComponent.harvestConfig.requiredSkillLevel)
        {
            Debug.Log("I have to low skill level to do it");
            return true;//temp
        }

        if (harvestComponent.harvestProgress >= harvestComponent.harvestConfig.harvestDifficulty)
        {
            Debug.Log("Harvesting finished!");
            harvestComponent.DropLoot();
            OrderPickUp orderPickUp = new OrderPickUp(GridNavigation.PositionToCoords(harvestComponent.transform.position), unitComponent);
            unitComponent.ordersBehaviour.AddOrder(orderPickUp);
            return true;//temp
        }

        Vector3Int unitCoords = GridNavigation.PositionToCoords(unitComponent.transform.position);
        Vector3Int harvestObjectCoords = GridNavigation.PositionToCoords(harvestComponent.transform.position);
        Vector3 nearestCellPosition = GridNavigation.GetNearestFreeCellPosition(harvestObjectCoords, unitComponent.transform.position);

        if (!GridNavigation.IsAdjacent(harvestObjectCoords, unitCoords))
        {
            OrderMoveTo orderMoveTo = new OrderMoveTo(nearestCellPosition, unitComponent);
            OrderHarvest orderHarvest = new OrderHarvest(harvestComponent, unitComponent);

            unitComponent.ordersBehaviour.AddOrder(orderMoveTo);
            unitComponent.ordersBehaviour.AddOrder(orderHarvest);
            return true;//temp
        }

        return false;//temp
    }
}
