using System.Collections.Generic;
using UnityEngine;

public class Snaketail : MonoBehaviour
{

    [SerializeField] Animator _animator;

    public void Move(Vector3 position, Vector2Int direction)
    {
        if(StringConsts._directionTriggerMap.TryGetValue(direction, out string trigger))
        {
            _animator.SetTrigger(trigger);
        }
        
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Snakehead>(out Snakehead snakehead))
        {
            LevelManager.Instance.EndScene(Reason.AteEnemy ,snakehead.IsFirst);
        }
    }
}
