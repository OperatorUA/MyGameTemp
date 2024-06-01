using UnityEngine;

public class SearchButton : MonoBehaviour
{
    public int radius;
    public BaseUnit selectedUnit;

    public void OnClick()
    {
        Vector3Int unitCoords = GridNavigation.PositionToCoords(selectedUnit.transform.position);
        GridCell cell = GridManager.GetNearestCellWithComponent<BaseResource>(unitCoords, radius);
        if (cell != null)
        {
            HarvestComponent harvestComponent = cell.objectOnCell.GetComponent<HarvestComponent>();
            HarvestTask harvestTask = new HarvestTask(selectedUnit, harvestComponent);
            selectedUnit.ordersBehaviour.AddOrder(harvestTask);
        } else
        {
            Debug.Log("I cant see any objects in this range!");
        }
    }
}
