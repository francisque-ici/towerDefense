using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [Header("Defender Stats")]
    public float attackRange = 5f;
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    private float fireCooldown = 0f;
    private Enemy targetEnemy;

    private void Update()
    {
        fireCooldown -= Time.deltaTime;

        // Tìm enemy gần nhất
        targetEnemy = FindClosestEnemy();

        if (targetEnemy != null && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate; // Reset cooldown
        }
    }

    private Enemy FindClosestEnemy()
    {
        List<Enemy> enemies = EnemyData.Instance.GetActiveEnemies(); // Tìm tất cả enemy trên map
        Enemy closest = null;
        float shortestDistance = attackRange;

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = enemy;
            }
        }

        return closest; // Trả về enemy gần nhất trong tầm
    }

    private void Shoot()
    {
        if (targetEnemy == null) return;

        GameObject bulletGO = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.SetTarget(targetEnemy);
        }
    }
}
