using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private int _height = 8;
    [SerializeField] private int _width = 15;
    [SerializeField] private float _cellSize = 1.1f;
    [SerializeField] private GameObject _cell;
    [SerializeField] private GameObject Snake;

    private static GridSystem _grid;
    public static GridSystem Grid { get { return _grid; }}
    void Start()
    {
        _grid = gameObject.AddComponent<GridSystem>();
        _grid.Setup(_width, _height, _cellSize, _cell);
        GameObject temp = Instantiate(Snake);
        temp.GetComponent<Snakehead>().SetGrid(_grid);
    }
}
