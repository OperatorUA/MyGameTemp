using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestCmd : BaseCmd
{
    private HarvestComponent harvestComponent;
    private BaseUnit unitComponent;
    private int unitSkillLvl;
    public HarvestCmd(BaseUnit unitComponent, HarvestComponent harvestComponent)
    {
        this.harvestComponent = harvestComponent;
        this.unitComponent = unitComponent;
        unitSkillLvl = unitSkillLvl = unitComponent.expComponent.expConfig.skills[harvestComponent.harvestConfig.skillType].level;
    }

    public override void Execute()
    {
        if (unitSkillLvl < harvestComponent.harvestConfig.requiredSkillLevel)
        {
            Debug.Log("I have to low skill level to do it");
            CmdAborted.Invoke();
            return;
        }

        Vector3Int unitCoords = GridNavigation.PositionToCoords(unitComponent.transform.position);
        Vector3Int harvestObjectCoords = GridNavigation.PositionToCoords(harvestComponent.transform.position);

        if (!GridNavigation.IsAdjacent(harvestObjectCoords, unitCoords))
        {
            CmdAborted.Invoke();
            return;
        }

        if (harvestComponent.harvestProgress >= harvestComponent.harvestConfig.harvestDifficulty)
        {
            Debug.Log("Harvesting finished!");
            harvestComponent.DropLoot();
            CmdCompleted.Invoke();
            return;
        }

        harvestComponent.Harvesting(unitSkillLvl, unitComponent.expComponent);
    }
}
