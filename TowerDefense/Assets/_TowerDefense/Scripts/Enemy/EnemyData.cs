using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public static EnemyData Instance { get; private set; }

    private List<Enemy> activeEnemies = new List<Enemy>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void AddEnemy(Enemy enemy)
    {
        activeEnemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        activeEnemies.Remove(enemy);
        Debug.Log(activeEnemies.Count);
    }

    public List<Enemy> GetActiveEnemies()
    {
        return activeEnemies;
    }
}
