using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPickUp : BaseOrder
{
    private Vector3Int targetCoords;
    private BaseUnit unitComponent;
    public OrderPickUp(Vector3Int targetCoords, BaseUnit unitComponent)
    {
        this.targetCoords = targetCoords;
        this.unitComponent = unitComponent;
    }
    public override void Execute()
    {
        ItemsStorage itemsStorage = GridManager.GetCell(targetCoords).objectOnCell.GetComponent<ItemsStorage>();
        if (itemsStorage == null)
        {
            Debug.Log("There no more loot boxes");

            OnOrderCompleted.Invoke();
            return;//temp
        }

        if (GridNavigation.PositionToCoords(unitComponent.transform.position) != targetCoords)
        {
            OrderMoveTo orderMoveTo = new OrderMoveTo(targetCoords, unitComponent);
            unitComponent.ordersBehaviour.AddOrder(orderMoveTo); // Steps
            unitComponent.ordersBehaviour.AddOrder(this); // Steps

            OnOrderCompleted.Invoke();
        } else
        {
            itemsStorage.GetAll(unitComponent.inventoryComponent);

            OnOrderCompleted.Invoke();
        }
    }

}
