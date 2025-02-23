using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 10f;
    public int Damage = 50;
    public float Lifetime = 5f; // Tự hủy sau 5 giây

    private Vector2 moveDirection; // Hướng di chuyển

    public void SetTarget(Enemy enemy)
    {
        if (enemy != null)
        {
            // Tính toán hướng từ Bullet đến Enemy tại thời điểm bắn
            moveDirection = (enemy.transform.position - transform.position).normalized;
        }
        else
        {
            moveDirection = transform.right; // Nếu enemy null, bay thẳng theo hướng hiện tại
        }

        // Hủy đạn sau thời gian nhất định để tránh lỗi
        Destroy(gameObject, Lifetime);
    }

    private void FixedUpdate()
    {
        // Di chuyển theo hướng đã tính
        transform.position += (Vector3)moveDirection * Speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(Damage);
            }

            Destroy(gameObject); // Xóa viên đạn sau khi va chạm
        }
    }
}
