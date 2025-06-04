using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BulletWeaponController : Weapon
{
    [SerializeField] float attackSpeed;
    float timer = 0;
    [SerializeField] BulletLevelDataSO levelData;

    [SerializeField] GameObject bulletPrefab;

    PlayerController player;
    float damage;

   
    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
        ApplyLevelStats();
        ;
    }

    void Update()
    {
        if (timer < attackSpeed)
        {
            timer += Time.deltaTime;
            return;
        }
        timer = 0;
        SpawnBullet();
    }

    private void SpawnBullet()
    {
        int currentLevel = level;

        if (currentLevel == 1)
        {
            SpawnDirectionalBullet(player.isFacingRight ? Vector3.right : Vector3.left);
        }
        else if (currentLevel > 1 && currentLevel < 5)
        {
            SpawnDirectionalBullet(Vector3.right);
            SpawnDirectionalBullet(Vector3.left);
        }
        else if (currentLevel >= 5)
        {
            int bulletCount = 6;
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * (360f / bulletCount);
                Vector3 dir = Quaternion.Euler(0, 0, angle) * Vector3.right;
                SpawnDirectionalBullet(dir.normalized);
            }
        }
    }

    private void SpawnDirectionalBullet(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        var proj = bullet.GetComponent<BulletProjectile>();
        proj.SetDirection(direction);
        proj.SetDamage(damage);
        Destroy(bullet, 0.30f);
    }




    public override void ApplyLevelStats()
    {

        var currentLevel = levelData.levels[level];
        attackSpeed = currentLevel.attackSpeed;
        damage = currentLevel.Damage;
        
        
    }

    public override string GetNextLevelDescription()
    {
        return levelData.levels[level + 1].levelDescription;
    }
}
