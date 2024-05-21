using UnityEngine;

public class GridCell
{
    public GameObject cellObj;
    public Vector3Int coords;
    public bool isOcupated;
    public GameObject objectOnCell;

    public GridCell(GameObject cellObj, Vector3Int coords)
    {
        this.cellObj = cellObj;
        this.coords = coords;
        isOcupated = false;
    }
}
