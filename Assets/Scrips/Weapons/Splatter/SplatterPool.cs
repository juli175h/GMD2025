using System.Collections.Generic;
using UnityEngine;

public class SplatterPool : MonoBehaviour
{
    public float tickRate = 2f;
    public float duration = 5f;
    public float damage;

    private Dictionary<EnemyController, float> damageTimers = new Dictionary<EnemyController, float>();

    void Start()
    {
        Debug.Log("SplatterPool Started");
        Destroy(gameObject, duration);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                if (!damageTimers.ContainsKey(enemy))
                    damageTimers[enemy] = 0f;

                damageTimers[enemy] += Time.deltaTime;

                if (damageTimers[enemy] >= tickRate)
                {
                    enemy.TakeDamage(damage);
                    damageTimers[enemy] = 0f;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null && damageTimers.ContainsKey(enemy))
            {
                damageTimers.Remove(enemy);
            }
        }
    }
}
