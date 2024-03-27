using System.Collections.Generic;
using UnityEngine;

public class Snakehead : MonoBehaviour
{
    [Header("| Vectors ")]
    [Space(5)]
    [SerializeField] private Vector2Int _direction = Vector2Int.up;
    [SerializeField] private Vector2Int _lastDirection = Vector2Int.up;
    [SerializeField] private Vector2Int _position;

    // Hashmaps
    private Dictionary<KeyCode, Vector2Int> _inputDirectionMap = new()
    {
        { KeyCode.W, Vector2Int.up },
        { KeyCode.A, Vector2Int.left },
        { KeyCode.S, Vector2Int.down },
        { KeyCode.D, Vector2Int.right }
    };

    private Dictionary<Vector2Int, string> _directionTriggerMap = new()
    {
        { Vector2Int.up, StringConsts.UP },
        { Vector2Int.left, StringConsts.LEFT },
        { Vector2Int.down, StringConsts.DOWN },
        { Vector2Int.right, StringConsts.RIGHT }
    };

    [Space(15)]
    [Header("| Timer ")]
    [Space(5)]
    [SerializeField] private float _durationTime = 2f;
    [SerializeField] private float _elapsedTime;

    [Space(15)]
    [Header("| References ")]
    [Space(5)]
    [SerializeField] private Animator _animator;
    private GridSystem _grid;

    [Space(15)]
    [Header("| Prefabs ")]
    [Space(5)]
    [SerializeField] private Snakebody _snakeBody;
    [SerializeField] private Snaketail _snakeTailPrefab;
    private Snaketail _snakeTail;
    [SerializeField] private List<Snakebody> _currentBody = new List<Snakebody>();

    // Offset
    [SerializeField, Range(1, 10)] private int _bodySize = 3;

    public delegate void BodySizeChangedEventHandler(int newValue);
    public event BodySizeChangedEventHandler OnIntegerValueChanged;

    public int BodySize
    {
        get { return _bodySize; }
        set
        {
            if (_bodySize != value)
            {
                _bodySize = value;
                OnIntegerValueChanged?.Invoke(value);
            }
        }
    }


    private void Start()
    {
        OnIntegerValueChanged += ChangeBodySize;


        CreateBody();
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

    private void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _durationTime)
        {
            Move(_direction);
            _elapsedTime = 0f;
        }
    }

    private void CreateBody()
    {
        // Clear existing bodies and tail
        foreach (var body in _currentBody)
        {
            Destroy(body.gameObject);
        }
        if (_snakeTail != null)
        {
            Destroy(_snakeTail.gameObject);
        }

        _currentBody.Clear();

        // Creating Body
        for (int i = 0; i <= BodySize; i++)
        {
            Vector2Int bodyPosition = new(_position.x, _position.y - i);
            Coordinates bodyCoordinates = _grid.SetCoordinates(bodyPosition);

            if (i != BodySize)
            {
                _currentBody.Add (
                    Instantiate(_snakeBody, bodyCoordinates.gridPosition, transform.rotation)
                );
            }
            else
            {
                _snakeTail = Instantiate(_snakeTailPrefab, bodyCoordinates.gridPosition, transform.rotation);
            }
        }

        // Setting Tail
        _currentBody[BodySize - 1].ConnectTail(true, _snakeTail);

        // Connecting References
        for (int i = 0; i < _currentBody.Count - 1; i++)
        {
            _currentBody[i].SetNext(_currentBody[i + 1]);
        }
    }

    private void Move(Vector2Int direction)
    {
        _animator.SetTrigger(_directionTriggerMap[_direction]);

        // Telling child to move
        _currentBody[0].Move(transform.position, direction);

        _lastDirection = _direction;
        _position += direction;

        Coordinates updated = _grid.SetCoordinates(_position);

        _position = updated.clampedPosition;
        transform.position = updated.gridPosition;
    }

    private void ChangeBodySize(int newValue)
    {
        int currentSize = _currentBody.Count;
        if (newValue > currentSize)
        {
            // Add new bodies
            for (int i = 0; i <= newValue; i++)
            {
                Coordinates bodyCoordinates = _grid.SetCoordinates(_position);
                Snakebody snakeBody = Instantiate(_snakeBody, bodyCoordinates.gridPosition, transform.rotation);
                _currentBody.Add(snakeBody);
                snakeBody.SetNext(_currentBody[0]); // Connect the new body to the current first body
                _currentBody.Insert(0, snakeBody); // Insert the new body at the beginning of the list
            }
        }
        else if (newValue < currentSize)
        {
            // Remove existing bodies
            for (int i = 0; i < currentSize - newValue; i++)
            {
                int lastIndex = _currentBody.Count - 1;

                Snakebody removedBody = _currentBody[lastIndex];
                _currentBody.RemoveAt(lastIndex); 
                Destroy(removedBody.gameObject); 

                _currentBody[lastIndex - 1].SetNext(null);
            }

            _currentBody[BodySize - 1].ConnectTail(true, _snakeTail);
        }

        Debug.Log("Body size changed to: " + newValue);
    }


    // Restricting the snake from going back on it's neck
    private bool CanChangeDirection(Vector2Int newDirection)
    {
        return newDirection + _lastDirection != Vector2Int.zero;
    }

    public void SetUp(Vector2Int position, int initialSize, GridSystem grid)
    {
        _grid = grid;
        _position = position;
        _bodySize = initialSize;
    }

}