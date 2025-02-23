using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject EnemyPrefab;
    public int MinEnemies = 3;
    public int MaxEnemies = 7;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies()
    {
        int enemyCount = Random.Range(MinEnemies, MaxEnemies);
        for (int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSeconds(1);
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        GameObject newEnemyObject = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
        Enemy newEnemy = newEnemyObject.GetComponent<Enemy>();

        if (newEnemy != null)
        {
            newEnemy.Speed = 2;
            EnemyData.Instance.AddEnemy(newEnemy);
        }
    }
}
