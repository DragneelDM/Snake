using System.Collections.Generic;
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
                Instantiate(cell, GetWorldPosition(gridWidth, gridHeight), Quaternion.identity, transform);
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

    public SnakeCoordinates SetSnakeCoordinates(Vector2Int direction)
    {
        Vector2Int correctedValue;
        Vector3 newPostion;

        // Wrapping the Snake within Screen. Each if corresponding with each boundary
        if (direction.x % _width == 0 && direction.x != 0)
        {
            direction.x -= _width;
        }
        if (direction.y % _height == 0 && direction.y != 0)
        {
            direction.y -= _height;
        }
        if(direction.x < 0)
        {
            int lastRow = _width - 1;
            direction.x = lastRow;
        }
        if(direction.y < 0)
        {
            int lastColumn = _height - 1;
            direction.y = lastColumn;
        }

        correctedValue = new Vector2Int(direction.x, direction.y);
        newPostion = new Vector3(direction.x, direction.y) * _cellSize;

        return new SnakeCoordinates(correctedValue, newPostion);
    }
}

public struct SnakeCoordinates
{
    public Vector2Int CorrectedValue;
    public Vector3 NewPostion;

    public SnakeCoordinates(Vector2Int correctedValues, Vector3 newPostions)
    {
        CorrectedValue = correctedValues;
        NewPostion = newPostions;
    }

}