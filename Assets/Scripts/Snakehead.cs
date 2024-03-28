using System.Collections.Generic;
using UnityEngine;

public class Snakehead : MonoBehaviour
{
    [Header("| Vectors ")]
    [Space(5)]
    [SerializeField] private Vector2Int _direction = Vector2Int.up;
    [SerializeField] private Vector2Int _lastDirection = Vector2Int.up;
    [SerializeField] private Vector2Int _position;

    private Dictionary<KeyCode, Vector2Int> _inputMap;

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
    private List<Snakebody> _currentBody = new List<Snakebody>();

    // Offset
    [SerializeField, Range(1, 10)] private int _bodySize = 3;
    public bool IsFirst;

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
        _inputMap = IsFirst ? StringConsts._snake1InputMap : StringConsts._snake2InputMap;
    }

    private void Update()
    {
        foreach (var input in _inputMap)
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

        // Creating Body
        for (int i = 1; i <= BodySize; i++)
        {
            Vector2Int bodyPosition = new(_position.x, _position.y - i);
            Coordinates bodyCoordinates = _grid.SetCoordinates(bodyPosition);

            if (i != BodySize)
            {
                _currentBody.Add(
                    Instantiate(_snakeBody, bodyCoordinates.gridPosition, transform.rotation)
                );
            }
            else
            {
                _snakeTail = Instantiate(_snakeTailPrefab, bodyCoordinates.gridPosition, transform.rotation);
            }
        }

        // Setting Tail
        _currentBody[BodySize - 2].ConnectTail(_snakeTail);

        // Connecting References
        for (int i = 0; i < _currentBody.Count - 1; i++)
        {
            _currentBody[i].SetNext(_currentBody[i + 1]);
        }
    }

    private void Move(Vector2Int direction)
    {
        _animator.SetTrigger(StringConsts._directionTriggerMap[_direction]);

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
        if (newValue - currentSize < 0)
        {
            LevelManager.Instance.EndScene(Reason.NoMoreBody, IsFirst);
        }

        if (newValue > currentSize)
        {
            // Add new bodies
            for (int i = 0; i <= newValue - currentSize; i++)
            {
                Snakebody lastBody = _currentBody[_currentBody.Count - 1];
                lastBody.DeleteTail();
                Snakebody snakeBody = Instantiate(_snakeBody, lastBody.TailPosition(), lastBody.transform.rotation);
                snakeBody.SetNext(_currentBody[0]);
                _currentBody.Insert(0, snakeBody);
            }
            _currentBody[_currentBody.Count - 1].ConnectTail(_snakeTail);
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

            _currentBody[BodySize - 1].ConnectTail(_snakeTail);
        }

    }

    public void IncreaseSpeed(float speed)
    {
        _durationTime -= speed;
        if (_durationTime < 0)
        {
            LevelManager.Instance.EndScene(Reason.AteFood, IsFirst);
        }
    }

    // Restricting the snake from going back on it's neck
    private bool CanChangeDirection(Vector2Int newDirection)
    {
        return newDirection + _lastDirection != Vector2Int.zero;
    }

    public void SetUp(Vector2Int position, int initialSize, GridSystem grid, bool isFirst)
    {
        _grid = grid;
        _position = position;
        _bodySize = initialSize;
        IsFirst = isFirst;

    }

}