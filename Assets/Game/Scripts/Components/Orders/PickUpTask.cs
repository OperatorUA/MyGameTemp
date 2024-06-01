using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpTask : BaseTask
{
    public PickUpTask(BaseUnit unitComponent, Vector3Int targetCoords)
    {
        MoveCmd orderMoveTo = new MoveCmd(unitComponent, targetCoords);
        PickUpCmd pickUpCmd = new PickUpCmd(unitComponent.inventoryComponent.itemsStorage, targetCoords);

        orderMoveTo.parentTask = this;
        pickUpCmd.parentTask = this;

        subCmds.Add(orderMoveTo);
        subCmds.Add(pickUpCmd);
    }
}
