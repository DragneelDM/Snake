using UnityEngine;

public class Stage : MonoBehaviour
{
    [Header("| Grid Values ")]
    [Space(5)]
    [SerializeField] private int _height = 8;
    [SerializeField] private int _width = 15;
    [SerializeField] private float _cellSize = 1.1f;

    [Space(20)]
    [Header("| Snake Values ")]
    [Space(5)]
    [SerializeField] private Vector2Int _snake1StartPos = new(0, 1);
    [SerializeField] private Vector2Int _snake2StartPos = new(15, 1);
    [SerializeField] private int _snakeLength = 3;

    [Space(20)]
    [Header("| Prefabs ")]
    [Space(5)]
    [SerializeField] private GameObject _cell;
    [SerializeField] private Snakehead Snake1;
    [SerializeField] private Snakehead Snake2;

    private static GridSystem _grid;
    public static GridSystem Grid { get { return _grid; } }

    private void Awake()
    {
        _grid = gameObject.AddComponent<GridSystem>();
        _grid.Setup(_width, _height, _cellSize, _cell);

        Coordinates snake1Coordinates = _grid.SetCoordinates(_snake1StartPos);
        Coordinates snake2Coordinates = _grid.SetCoordinates(_snake2StartPos);

        Instantiate(Snake1, snake1Coordinates.gridPosition, Snake1.transform.rotation).SetUp(snake1Coordinates.clampedPosition, _snakeLength, _grid, true);
        Instantiate(Snake2, snake2Coordinates.gridPosition, Snake2.transform.rotation).SetUp(snake2Coordinates.clampedPosition, _snakeLength, _grid, false);
    }

    public int GetHeight()
    {
        return _height;
    }

    public int GetWidth()
    {
        return _width;
    }
}
