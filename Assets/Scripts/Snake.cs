using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private Vector2Int _direction = Vector2Int.up;
    [SerializeField] private Vector2Int _position = Vector2Int.zero;
    [SerializeField, Range(0.01f, 10f)] private float _speed = 0.1f;
    [SerializeField] private float _durationTime = 100f;
    [SerializeField] private float _elapsedTime;
    private bool _left, _right, _up, _down;

    private void Update()
    {
            _up = Input.GetKeyDown(KeyCode.W);
            _left = Input.GetKeyDown(KeyCode.A);
            _down = Input.GetKeyDown(KeyCode.S);
            _right = Input.GetKeyDown(KeyCode.D);

        if (_up)
        {
            _direction = Vector2Int.up;
        }
        else if (_left)
        {
            _direction = Vector2Int.left;
        }
        else if (_down)
        {
            _direction = Vector2Int.down;
        }
        else if (_right)
        {
            _direction = Vector2Int.right;
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

    public void Move(Vector2Int direction)
    {
        _position += direction;

        SnakeCoordinates updated = Stage.Grid.SetSnakeCoordinates(_position);

        _position = updated.CorrectedValue;
        transform.position = updated.NewPostion;
    }

}