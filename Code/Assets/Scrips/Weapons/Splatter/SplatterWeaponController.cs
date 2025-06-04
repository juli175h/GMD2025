using UnityEngine;

public class SplatterWeaponController : Weapon
{
    [SerializeField] float attackSpeed;
    float timer;

    [SerializeField] GameObject SplatterProjPrefab;
    [SerializeField] SplatterLevelDataSO levelData;

    PlayerController player;
    public float distance = 2.5f;

    float duration;
    float tickRate;
    float damage;
    float size;

    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
        ApplyLevelStats();
    }

    void Update()
    {
        if (level > 0)
        {
            if (timer < attackSpeed)
            {
                timer += Time.deltaTime;
                return;
            }
            timer = 0;
            SpawnSplatterProj();
        }
    }

    private void SpawnSplatterProj()
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        Vector3 targetPoint = transform.position + new Vector3(randomDir.x, randomDir.y, 0f) * distance;

        GameObject proj = Instantiate(SplatterProjPrefab, transform.position, Quaternion.identity);
        SplatterProjectile splatterProj = proj.GetComponent<SplatterProjectile>();
        splatterProj.target = targetPoint;

        // Pass level stats to projectile
        splatterProj.duration = duration;
        splatterProj.tickRate = tickRate;
        splatterProj.damage = damage;
        splatterProj.size = size;

    }

    public override void ApplyLevelStats()
    {
        
            var currentLevel = levelData.levels[level];
            attackSpeed = currentLevel.attackSpeed;

            duration = currentLevel.duration;
            tickRate = currentLevel.tickRate;
            damage = currentLevel.damage;
            size = currentLevel.size;
        
        
    }

    public override string GetNextLevelDescription()
    {
        return levelData.levels[level + 1].levelDescription;
    }
}
