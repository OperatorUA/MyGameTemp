using UnityEngine;

public class SearchButton : MonoBehaviour
{
    public int radius;
    public BaseUnit selectedUnit;

    public void OnClick()
    {
        Vector3Int unitCoords = GridNavigation.PositionToCoords(selectedUnit.transform.position);
        GridCell cell = GridManager.GetNearestCellWithComponent<BaseResource>(radius, unitCoords);

        if (cell != null )
        {
            HarvestComponent harvestComponent = cell.objectOnCell.GetComponent<HarvestComponent>();
            OrderHarvest orderHarvest = new OrderHarvest(harvestComponent, selectedUnit);
            selectedUnit.ordersBehaviour.AddOrder(orderHarvest);
        } else
        {
            Debug.Log("I cant see any objects in this range!");
        }
    }
}
