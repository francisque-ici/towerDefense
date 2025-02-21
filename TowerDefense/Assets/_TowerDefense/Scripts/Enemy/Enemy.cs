using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float Health = 100;
    public float Speed = 3.0f;
    public float Shield = 0;
    public float HealthRegen = 0;
    public int Reward;

    private float CurentHealth;
    private float CurrentShield;
    private int currentWaypoint = 0;

    private List<Vector2Int> Path;

    private void Start()
    {
        CurentHealth = Health;
        CurrentShield = Shield;

        Path = PathFinder.Instance.GetPath();
    }

    private void Update()
    {
        MoveAlongPath();
    }

    private void MoveAlongPath()
    {
        if (Path == null || currentWaypoint >= Path.Count) return;

        Vector2Int WaypointPosition = Path[currentWaypoint];
        Vector3 TargetPosition = new Vector3(GridManager.StartCorner.x + WaypointPosition.x, GridManager.StartCorner.y + WaypointPosition.y, 0);
        Debug.Log(TargetPosition);
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, Speed * Time.deltaTime);

        // Nếu đến gần waypoint hiện tại, chuyển sang waypoint tiếp theo
        if (Vector3.Distance(transform.position, TargetPosition) < 0.01f)
        {
            currentWaypoint++;
            if (currentWaypoint >= Path.Count)
            {
                Kill();
            }
        }
    }

    private void Kill()
    {
        EnemySpawner.Instance.SpawnEnemy();
        Destroy(transform.gameObject);
    }
}
