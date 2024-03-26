using UnityEngine;

public class GridSystem : MonoBehaviour
{
    private int _width;
    private int _height;
    private float _cellSize;
    private int[,] gridArray;

    public void Setup(int width, int height, float cellSize, GameObject cell)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;

        gridArray = new int[width, height];

        for (int gridWidth = 0; gridWidth < gridArray.GetLength(0); gridWidth++)
        {
            for (int gridHeight = 0; gridHeight < gridArray.GetLength(1); gridHeight++)
            {
                Debug.DrawLine(GetWorldPosition(gridWidth, gridHeight), GetWorldPosition(gridWidth, gridHeight + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(gridWidth, gridHeight), GetWorldPosition(gridWidth + 1, gridHeight), Color.white, 100f);
                Instantiate(cell, GetWorldPosition(gridWidth, gridHeight), cell.transform.rotation, transform);
            }
        }

        Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.white, 100f);
    }

    public Vector3 GetWorldPosition(int width, int height)
    {
        if (width >= _width)
        {
            width -= _width;
        }
        if (height >= _height)
        {
            height -= _height;
        }

        return new Vector3(width, height) * _cellSize;
    }

    public Coordinates SetCoordinates(Vector2Int currentPosition)
    {
        Vector2Int clampedPosition;
        Vector3 gridPostion;

        // Wrapping the Snake within Screen. Each if statement corresponds with each boundary
        if (currentPosition.x % _width == 0 && currentPosition.x != 0)
        {
            currentPosition.x -= _width;
        }
        if (currentPosition.y % _height == 0 && currentPosition.y != 0)
        {
            currentPosition.y -= _height;
        }
        if(currentPosition.x < 0)
        {
            int lastRow = _width - 1;
            currentPosition.x = lastRow;
        }
        if(currentPosition.y < 0)
        {
            int lastColumn = _height - 1;
            currentPosition.y = lastColumn;
        }

        clampedPosition = new Vector2Int(currentPosition.x, currentPosition.y);
        gridPostion = new Vector3(currentPosition.x, currentPosition.y) * _cellSize;

        return new Coordinates(clampedPosition, gridPostion);
    }
}

public struct Coordinates
{
    public Vector2Int clampedPosition;
    public Vector3 gridPosition;

    public Coordinates(Vector2Int correctedValues, Vector3 newPostions)
    {
        clampedPosition = correctedValues;
        gridPosition = newPostions;
    }

}