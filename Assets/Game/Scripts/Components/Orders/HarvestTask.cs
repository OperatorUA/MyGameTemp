using UnityEngine;

public class HarvestTask : BaseTask
{
    public HarvestTask(BaseUnit unitComponent, HarvestComponent harvestComponent)
    {
        Vector3 targetPosition = GridNavigation.GetCellCenterPosition(harvestComponent.transform.position);
        Vector3 nearestCellPosition = GridNavigation.GetNearestFreeCellPosition(targetPosition, unitComponent.transform.position);

        MoveCmd moveCmd = new MoveCmd(unitComponent, nearestCellPosition);
        HarvestCmd harvestCmd = new HarvestCmd(unitComponent, harvestComponent);
        PickUpTask pickUpTask = new PickUpTask(unitComponent, GridNavigation.PositionToCoords(targetPosition));

        moveCmd.parentTask = this;
        harvestCmd.parentTask = this;
        pickUpTask.parentTask = this;

        subCmd.Add(moveCmd);
        subCmd.Add(harvestCmd);
        subCmd.Add(pickUpTask);
    }
}
