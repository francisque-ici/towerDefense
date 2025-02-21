using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance {get; private set;}

    [Header("Spawner Settings")]
    public GameObject EnemyPrefab;
    public int MinEnemies = 3;
    public int MaxEnemies = 7; 

    private Transform SpawnPoint;

    private List<GameObject> ActiveEnemies = new List<GameObject>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
    }


    public void SpawnEnemies(Transform _SpawnPoint)
    {
        SpawnPoint = _SpawnPoint;
        int EnemyCount = Random.Range(MinEnemies, MaxEnemies);

        for (int i = 0; i < EnemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(EnemyPrefab, SpawnPoint.position, Quaternion.identity);
        newEnemy.GetComponent<Enemy>().Speed = Random.Range(1, 5);
        ActiveEnemies.Add(newEnemy);
    }

    public List<GameObject> GetActiveEnemies()
    {
        return ActiveEnemies; 
    }
}
