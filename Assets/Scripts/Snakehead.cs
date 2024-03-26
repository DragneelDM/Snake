using System.Collections.Generic;
using UnityEngine;

public class Snakehead : MonoBehaviour
{
    [Header(" Vectors ")]
    [Space (5)]
    [SerializeField] private Vector2Int _direction = Vector2Int.up;
    [SerializeField] private Vector2Int _lastDirection = Vector2Int.up;
    [SerializeField] private Vector2Int _position = Vector2Int.zero;

    // Hashmaps
    private Dictionary<KeyCode, Vector2Int> _inputDirectionMap = new Dictionary<KeyCode, Vector2Int>()
    {
        { KeyCode.W, Vector2Int.up },
        { KeyCode.A, Vector2Int.left },
        { KeyCode.S, Vector2Int.down },
        { KeyCode.D, Vector2Int.right }
    };

    private Dictionary<Vector2Int, string> _directionTriggerMap = new Dictionary<Vector2Int, string>()
    {
        { Vector2Int.up, StringConsts.UP },
        { Vector2Int.left, StringConsts.LEFT },
        { Vector2Int.down, StringConsts.DOWN },
        { Vector2Int.right, StringConsts.RIGHT }
    };

    [Space (15)]
    [Header(" Timer ")]
    [Space (5)]
    [SerializeField] private float _durationTime = 2f;
    [SerializeField] private float _elapsedTime;

    [Space (15)]
    [Header(" References ")]
    [Space (5)]
    [SerializeField] private Animator _animator;
    private GridSystem _grid;

    [Space (15)]
    [Header(" Prefabs ")]
    [Space (5)]
    [SerializeField] private Snakebody _snakeBody;
    [SerializeField] private List<Snakebody> _currentBody = new List<Snakebody>();

    // offset
    private int _snakeTailOffset = 1;

    private void Start()
    {
        Vector2Int bodyPosition = new(_position.x, _position.y - _snakeTailOffset);
        Coordinates bodyCoordinates = _grid.SetCoordinates(bodyPosition);
        Snakebody tempref = Instantiate(_snakeBody, bodyCoordinates.gridPosition, transform.rotation);

        tempref.SetGrid(_grid);
        _currentBody.Add(tempref);
    }

    private void Update()
    {
        foreach (var input in _inputDirectionMap)
        {
            if (Input.GetKeyDown(input.Key))
            {
                if (CanChangeDirection(input.Value))
                {
                    _direction = input.Value;
                    break;
                }
            }
        }
    }

    // Check if new direction is not opposite to current direction,  thus restricting the snake from going reverse
    private bool CanChangeDirection(Vector2Int newDirection)
    {
        return newDirection + _lastDirection != Vector2Int.zero;
    }

    private void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _durationTime)
        {
            Move(_direction);
            _elapsedTime = 0f;
        }
    }

    private void Move(Vector2Int direction)
    {
        _animator.SetTrigger(_directionTriggerMap[_direction]);

        // Telling child to move
        _currentBody[0].Move(_position, direction);

        _lastDirection = _direction;
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