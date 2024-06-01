using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCmd : BaseCmd
{
    private Vector3Int targetCoords;
    private ItemsStorage reciverStorage;
    public PickUpCmd(ItemsStorage reciverStorage, Vector3Int targetCoords)
    {
        this.targetCoords = targetCoords;
        this.reciverStorage = reciverStorage;
    }
    public override void Execute()
    {
        ItemsStorage itemsStorage = GridManager.GetCell(targetCoords).objectOnCell.GetComponent<ItemsStorage>();

        if (itemsStorage != null)
        {
            itemsStorage.SendAllItems(reciverStorage);
            CmdCompleted.Invoke();
        } else
        {
            Debug.Log("There no more loot boxes");
            CmdCompleted.Invoke();
            return;
        }
    }
}
