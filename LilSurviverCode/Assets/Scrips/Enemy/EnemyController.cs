using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public float speed;
    [HideInInspector] public float originalSpeed;
    float health;
    [SerializeField] int maxHealth;
    [SerializeField] GameObject expPrefab;
    public float Damage;

    public Bar healthbar;

    private bool isFacingRight = true;
    Rigidbody2D rg;
    [HideInInspector] public bool isPushedBack = false;
    [HideInInspector] public float pushBackTimer = 0f;

    private void Awake()
    {
        originalSpeed = speed;
        rg = GetComponent<Rigidbody2D>();
        health = maxHealth;
        healthbar.SetMaxValue(maxHealth);
        healthbar.SetValue(maxHealth);

    }


    private void FixedUpdate()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        rg.linearVelocity = direction * speed;

        if (direction.x > 0 && !isFacingRight)
            Flip();
        else if (direction.x < 0 && isFacingRight)
            Flip();
    }



    public void TakeDamage(float damage)
    {
        health = health - damage;
        healthbar.SetValue(health);
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject exp = Instantiate(expPrefab);
        exp.transform.position = transform.position;
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
