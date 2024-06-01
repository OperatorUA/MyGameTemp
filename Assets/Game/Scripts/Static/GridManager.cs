using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private static Dictionary<Vector3Int, GridCell> _gridCells = new Dictionary<Vector3Int, GridCell>();

    public Material gridCellMaterial;
    public bool showGrid;

    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        for (int z = 0; z < WorldGenerator.worldSize.z; z++)
        {
            for (int x = 0; x < WorldGenerator.worldSize.x; x++)
            {
                Vector3 position = new Vector3(x, 0, z) + transform.position;
                CreateCell(GridNavigation.PositionToCoords(position));
            }
        }
    }
    private void CreateCell(Vector3Int coords)
    {
        GameObject cellObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
        cellObj.name = $"Cell: {coords.x}; {coords.z}";

        cellObj.transform.position = GridNavigation.GetCellCenterPosition(coords) + Vector3.up * 0.01f;
        cellObj.transform.rotation = Quaternion.Euler(Vector3.right * 90);
        cellObj.transform.SetParent(transform);

        Renderer renderer = cellObj.GetComponent<Renderer>();
        renderer.material = gridCellMaterial;
        renderer.material.color = ColorByCoords(coords);

        GridCell cell = new GridCell(cellObj, coords);
        _gridCells[coords] = cell;
    }

    public static GridCell GetCell(Vector3 positionOrCoords, bool log = false)
    {
        GridCell result = null;
        Vector3Int coords = GridNavigation.PositionToCoords(positionOrCoords);

        if (_gridCells.ContainsKey(coords)) result = _gridCells[coords];
        else
        {
            if (log)
            {
                Debug.LogWarning($"Cant get cell by {positionOrCoords} position in {coords} coords");
            }
        }

        return result;
    }

    public static GridCell GetNearestCellWithComponent<T>(Vector3Int center, int range = int.MaxValue)
    {
        if (range > WorldGenerator.worldSize.x) range = Mathf.RoundToInt(WorldGenerator.worldSize.x / 2f);
        if (range > WorldGenerator.worldSize.z) range = Mathf.RoundToInt(WorldGenerator.worldSize.z / 2f);

        List<GridCell> cellsInRange = new List<GridCell>();

        for (int x = -range; x < range; x++)
        {
            for (int z = -range; z < range; z++)
            {
                Vector3Int currentCoords = new Vector3Int(x, 0, z) + center;
                
                float distance = Vector3.Distance(center, currentCoords);

                if (distance <= range)
                {
                    GridCell gridCell = GridManager.GetCell(currentCoords);

                    if (gridCell != null && gridCell.objectOnCell != null)
                    {
                        T component = gridCell.objectOnCell.GetComponent<T>();
                        if (component != null) cellsInRange.Add(gridCell);
                    }
                }
            }
        }

        GridCell nearestCell = null;
        float minDistance = float.MaxValue;
        foreach (GridCell gridCell in cellsInRange)
        {
            float distance = Vector3.Distance(center, gridCell.coords);
            if (distance <= minDistance)
            {
                minDistance = distance;
                nearestCell = gridCell;
            }
        }

        return nearestCell;
    }

    private static Color ColorByCoords(Vector3Int coords)
    {
        Color cellColor = new Color();

        if ((coords.x + coords.z) % 2 == 0) cellColor = Color.white;
        else cellColor = Color.black;
        cellColor.a = 0.05f;

        return cellColor;
    }

    private static GridCell hoveredCell;
    public static void HoverCell(Vector3 position)
    {
        GridCell newCell = GetCell(position, true);

        if (newCell != hoveredCell)
        {
            if (hoveredCell != null)
            {
                hoveredCell.cellObj.GetComponent<Renderer>().material.color = ColorByCoords(hoveredCell.coords);
            }

            newCell.cellObj.GetComponent<Renderer>().material.color = Color.green;
            hoveredCell = newCell;
        }

        GUIManager.Log($"x:{hoveredCell.coords.x} \nz:{hoveredCell.coords.z}");
    }

    public static void OcupateCell(Vector3Int coords, GameObject obj)
    {
        _gridCells[coords].isOcupated = true;
        _gridCells[coords].objectOnCell = obj;
    }
    public static void DisocupateCell(Vector3Int coords)
    {
        _gridCells[coords].isOcupated = false;
        _gridCells[coords].objectOnCell = null;
    }

    public static bool isCellFree(Vector3Int coords)
    {
        return !_gridCells[coords].isOcupated;
    }
}
