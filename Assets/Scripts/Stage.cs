using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private int _height = 8;
    [SerializeField] private int _width = 15;
    [SerializeField] private float _cellSize = 1.1f;
    [SerializeField] private Vector2Int _startPos = new(0, 1);
    [SerializeField] private int _snakeLength = 3;
    [SerializeField] private GameObject _cell;
    [SerializeField] private Snakehead Snake;
    [SerializeField] private ItemSpawner _itemSpawner;

    private static GridSystem _grid;
    public static GridSystem Grid { get { return _grid; } }

    void Awake()
    {
        _grid = gameObject.AddComponent<GridSystem>();
        _grid.Setup(_width, _height, _cellSize, _cell);
        Instantiate(Snake).SetUp(_startPos, _snakeLength, _grid);
    }
}
