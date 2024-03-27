using System.Collections.Generic;
using UnityEngine;

public class Snakebody : MonoBehaviour
{
    // References
    [SerializeField] private Animator _animator;
    [SerializeField] private Snaketail _snakeTail;
    private Snakebody _nextBody;

    // Directions
    private Vector2Int _lastDirection = Vector2Int.up;

    // Saved all possible Rotations
    private Dictionary<(Vector2Int, Vector2Int), string> _directionTriggerMap = new()
    {
        {(Vector2Int.up, Vector2Int.right), StringConsts.RUP},
        {(Vector2Int.up, Vector2Int.left), StringConsts.LUP},
        {(Vector2Int.down, Vector2Int.right), StringConsts.RDOWN},
        {(Vector2Int.down, Vector2Int.left), StringConsts.LDOWN},
        {(Vector2Int.left, Vector2Int.up), StringConsts.RDOWN},
        {(Vector2Int.left, Vector2Int.down), StringConsts.RUP},
        {(Vector2Int.right, Vector2Int.up), StringConsts.LDOWN},
        {(Vector2Int.right, Vector2Int.down), StringConsts.LUP}
    };

    // Boolean
    private bool _isLast = false;

    public void Move(Vector3 position, Vector2Int direction)
    {
        if (direction == _lastDirection)
        {
            if (direction == Vector2Int.up || direction == Vector2Int.down)
            {
                _animator.SetTrigger(StringConsts.UP);
            }
            else if (direction == Vector2Int.left || direction == Vector2Int.right)
            {
                _animator.SetTrigger(StringConsts.SIDE);
            }
        }
        else

        if (_directionTriggerMap.TryGetValue((_lastDirection, direction), out string animTrigger))
        {
            _animator.SetTrigger(animTrigger);
        }

        if (_isLast)
        {
            _snakeTail.Move(transform.position, _lastDirection);
        }
        else
        {
            _nextBody?.Move(transform.position, _lastDirection);
        }

        _lastDirection = direction;

        transform.position = position;
    }

    public void ConnectTail(Snaketail snaketail)
    {
        _isLast = true;
        _snakeTail = snaketail;
    }

    public void DeleteTail()
    {
        _isLast = false;
        _snakeTail = null;
    }

    public Vector3 TailPosition()
    {
        return new Vector3(transform.position.x + _lastDirection.x, transform.position.y + _lastDirection.y);
    }

    public void SetNext(Snakebody nextBody)
    {
        _nextBody = nextBody;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Snakehead>(out Snakehead snakehead))
        {
            LevelManager.Instance.EndScene();
        }
    }
}