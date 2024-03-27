using System.Collections.Generic;
using UnityEngine;

public class Snaketail : MonoBehaviour
{
    private Vector2Int _position;
    private Vector2Int _lastDirection = Vector2Int.up;
    private Dictionary<Vector2Int, string> _directionTriggerMap = new()
    {
        { Vector2Int.up, StringConsts.UP },
        { Vector2Int.left, StringConsts.LEFT },
        { Vector2Int.down, StringConsts.DOWN },
        { Vector2Int.right, StringConsts.RIGHT }
    };

    [SerializeField] Animator _animator;

    public void Move(Vector3 position, Vector2Int direction)
    {
        if(_directionTriggerMap.TryGetValue(direction, out string trigger))
        {
            _animator.SetTrigger(trigger);
        }
        
        _lastDirection = direction;
        transform.position = position;
    }
}
