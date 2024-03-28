using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField, Range(-10, 10)] private int _value = 2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Snakehead>(out Snakehead snakehead))
        {
            snakehead.BodySize += _value;
            snakehead.IncreaseSpeed(_value * 0.1f);
            LevelManager.Instance.UpdateUI(_value, snakehead.IsFirst);
            Destroy(gameObject);
        }
    }
}
