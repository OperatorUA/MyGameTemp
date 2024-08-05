using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCmd : BaseCmd
{
    private BaseUnit unitComponent;

    public IdleCmd(BaseUnit unitComponent)
    {
        this.unitComponent = unitComponent;
    }

    public override void Execute()
    {
        Debug.Log("Idle");
        if (unitComponent.ordersBehaviour.orders.Count > 1)
        {
            CmdCompleted?.Invoke();
        }
    }
}
