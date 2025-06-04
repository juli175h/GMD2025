using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    Vector3 direction;
    [SerializeField] float speed;
    private float damage;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    public void SetDamage(float value)
    {
        damage = value;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}
