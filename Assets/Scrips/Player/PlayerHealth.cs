using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth;
    public float health;
    [SerializeField] GameManagerScript gameManager;

    public Bar healthbar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        healthbar.SetMaxValue(maxHealth);
        healthbar.SetValue(maxHealth);

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthbar.SetValue(health);
        SoundManager.PlaySound(SoundType.HIT);
        if (health <= 0)
        {
           
            gameManager.GameOver(false);
        }
        
    }
}
