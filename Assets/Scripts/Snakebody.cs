using System.Collections.Generic;
using UnityEngine;

public class Snakebody : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [HideInInspector] public Snaketail SnakeTail;
    private Vector2Int _position;
    private Vector2Int _lastDirection = Vector2Int.up;
    private GridSystem _grid;
    public bool IsFirst = false;
    private int _snakeTailOffset = 1;

    private void Start()
    {
        if (IsFirst)
        {
            CreateTail();
        }
    }

    private void CreateTail()
    {
        Vector2Int bodyPosition = new(_position.x, _position.y - _snakeTailOffset);
        Coordinates tailCoordinates = _grid.SetCoordinates(bodyPosition);
        Snaketail tempref = Instantiate(SnakeTail, tailCoordinates.gridPosition, transform.rotation);

        tempref.SetGrid(_grid);
    }

    public void setPosition(Vector2Int position)
    {
        _position = position;
    }

    public void Move(Vector2Int position, Vector2Int direction)
    {
        if (direction != _lastDirection)
        {
            // it is either up or down
            if (_lastDirection == Vector2Int.up && direction == Vector2Int.right)
            {
                _animator.SetTrigger(StringConsts.RUP);
            }
            else if(_lastDirection == Vector2Int.down && direction == Vector2Int.right)
            {
                _animator.SetTrigger(StringConsts.RDOWN);
            }
            else if(_lastDirection == Vector2Int.up && direction == Vector2Int.left)
            {
                _animator.SetTrigger(StringConsts.LUP);
            }
            else if(_lastDirection == Vector2Int.down && direction == Vector2Int.left)
            {
                _animator.SetTrigger(StringConsts.LDOWN);
            }
        }

        _position = position;
        Coordinates updated = _grid.SetCoordinates(_position);

        _position = updated.clampedPosition;
        transform.position = updated.gridPosition;
    }

    public void SetGrid(GridSystem grid)
    {
        _grid = grid;
    }
}


public struct Directions
{
    public Vector2Int LastDirection;
    public Vector2Int NewDirection;
}