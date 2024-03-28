using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private Stage _stage;
    [SerializeField] private Item _feedables;
    [SerializeField] private Item _massGainer;
    [SerializeField] private float _elaspsedTime;
    [SerializeField] private int _massGainerCounter = 3;
    [SerializeField] private int _massGainerIteration = 0;
    [SerializeField, Range(1,10)] private float _minDurationTime = 10f;
    
    private void Update()
    {
        RandomSpawn();
    }


    private void RandomSpawn()
    {
        // Position
        int height = Mathf.RoundToInt(Random.Range(1, _stage.GetHeight()));
        int width = Mathf.RoundToInt(Random.Range(1, _stage.GetWidth()));  

        float spawnTime = Random.Range(_minDurationTime, 15f);
        _elaspsedTime += Time.deltaTime;


        if(_elaspsedTime > spawnTime)
        {
            Coordinates coordinates = Stage.Grid.SetCoordinates(new Vector2Int(width, height));

            Item instantiate = _massGainerCounter > _massGainerIteration ? _massGainer : _feedables;
            _massGainerIteration++;

            Instantiate(instantiate, coordinates.gridPosition, Quaternion.identity);

            _elaspsedTime = 0;
        }
    }
    
}
