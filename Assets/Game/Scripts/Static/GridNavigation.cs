using UnityEngine;

public class GridNavigation
{
    public static Vector3 cellSize = new Vector3(1f, 0, 1f); // Пока бесполезно

    public static Vector3Int PositionToCoords(Vector3 position)
    {
        int xPos = Mathf.FloorToInt(position.x);
        int yPos = Mathf.FloorToInt(position.y + 0.001f); // temp
        int zPos = Mathf.FloorToInt(position.z);
        return new Vector3Int(xPos, yPos, zPos);
    }
    public static Vector3 GetCellCenterPosition(Vector3Int coords)
    {
        return coords + cellSize / 2f;
    }
    public static Vector3 GetCellCenterPosition(Vector3 position)
    {
        return PositionToCoords(position) + cellSize / 2f;
    }

    public static Vector3 GetNearestFreeCellPosition(Vector3Int coords, Vector3 unitPosition)
    {
        if (GridManager.isCellFree(coords)) return coords;

        float minDistance = float.MaxValue;
        Vector3 result = Vector3Int.zero;

        for (int x = coords.x - 1; x <= coords.x + 1; x++)
        {
            for (int z = coords.z - 1; z <= coords.z + 1; z++)
            {
                if (x == coords.x && z == coords.z) continue;

                Vector3Int currentCellCoords = new Vector3Int(x, 0, z);
                Vector3 cellCenterPosition = GetCellCenterPosition(currentCellCoords);
                
                float distance = Vector3.Distance(cellCenterPosition, unitPosition);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    result = cellCenterPosition;
                }
            }
        }
        
        return result;
    }

    public static bool IsAdjacent(Vector3Int coords1, Vector3Int coords2)
    {
        int dx = Mathf.Abs(coords1.x - coords2.x);
        int dz = Mathf.Abs(coords1.z - coords2.z);

        return (dx <= 1 && dz <= 1);
    }
}
