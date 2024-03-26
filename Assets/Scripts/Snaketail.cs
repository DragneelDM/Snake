using UnityEngine;

public class Snaketail : MonoBehaviour
{
    private Vector2Int _position;
    private Vector2Int _currentDirection;
    private Vector2Int _nextDirection;
    private GridSystem _grid;

    public void setPosition(Vector2Int position)
    {
        _position = position;
    }

    public void Move(Vector2Int direction)
    {
        _nextDirection = direction;
        if(_currentDirection != _nextDirection)
        {
            
        }
        _position += direction;

        Coordinates updated = _grid.SetCoordinates(_position);

        _position = updated.clampedPosition;
        transform.position = updated.gridPosition;
    }

    public void SetGrid(GridSystem grid)
    {
        _grid = grid;
    }
}
