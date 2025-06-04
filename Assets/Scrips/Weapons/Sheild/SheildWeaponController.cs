using System;
using UnityEngine;

public class SheildWeaponController : Weapon
{
    float attackSpeed;
    float duration;
    [SerializeField] SheildLevelDataSO levelData;
    float damage;
    bool isActive = false;
    float timer;
    PlayerController player;

    public override void ApplyLevelStats()
    {
        
            var currentLevel = levelData.levels[level];
            attackSpeed = currentLevel.attackSpeed;
            duration = currentLevel.duration;
            damage = currentLevel.damage;
        
        if(level >0)
            Activate();
        
    }

    void Awake()
    {
        player = GetComponentInParent<PlayerController>();
        isActive = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        player.hasImmunity = false;
        ApplyLevelStats();
    }

    // Update is called once per frame
    void Update()
    {
        if(level > 0)
        {
            
            if (isActive)
            {
                if (timer < duration)
                {
                    timer += Time.deltaTime;
                    return;
                }
                DeActivate();

            }
            else
            {
                if (timer < attackSpeed)
                {
                    timer += Time.deltaTime;
                    return;
                }
                Activate();
            }
        }
        
    }


    private void Activate()
    {
        timer = 0;
        isActive = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        player.hasImmunity = true;
    }

    private void DeActivate()
    {
        timer = 0;
        isActive = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        player.hasImmunity = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            enemy.TakeDamage(damage);
            
        }
    }

    public override string GetNextLevelDescription()
    {
        return levelData.levels[level + 1].levelDescription;
    }
}
