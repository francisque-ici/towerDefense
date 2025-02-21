using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance {get; private set;}
    public List<GameObject> Maps = new List<GameObject>();

    public GameObject CurrentMap;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;

        NewGame();
    }

    void NewGame()
    {
        LoadMap();
    }

    void LoadMap()
    {
        int ChoosenIndex = UnityEngine.Random.Range(0, Maps.Count);
        CurrentMap = Instantiate(Maps[ChoosenIndex], new Vector3(0, 0, 0), Quaternion.identity);

        Vector3 BasePosition = CurrentMap.transform.Find("Base").position;
        Vector3 SpawnPosition = CurrentMap.transform.Find("Spawn").position;

        GridManager.Instance.UpdateGrid(CurrentMap.transform.Find("PlaceZone").gameObject);
        PathFinder.Instance.NewPath(GridManager.Grid, 
        new Vector2Int(Mathf.RoundToInt(SpawnPosition.x - GridManager.StartCorner.x), Mathf.RoundToInt(SpawnPosition.y - GridManager.StartCorner.y)),
        new Vector2Int(Mathf.RoundToInt(BasePosition.x - GridManager.StartCorner.x), Mathf.RoundToInt(BasePosition.y - GridManager.StartCorner.y))
        );

        EnemySpawner.Instance.SpawnEnemies(CurrentMap.transform.Find("Spawn"));
    }

}
