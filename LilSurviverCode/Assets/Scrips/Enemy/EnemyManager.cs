using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemy_normal_Prefab;
    [SerializeField] GameObject enemy_speed_Prefab;
    [SerializeField] GameObject enemy_giant_Prefab;
    [SerializeField] Vector2 primarySpawnArea;
    [SerializeField] Vector2 secondarySpawnArea;
    [SerializeField] GamePhaseDataSO phaseData;
    public int gamePhase = 0;

    float enemy_normal_spawnTimer;
    float enemy_speed_spawnTimer;
    float enemy_giant_spawnTimer;
    private float currentTime;

    [SerializeField] Transform player;

    // Update is called once per frame

    private void Awake()
    {
        UpdatePhase(gamePhase);

    }
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime> phaseData.Phases[gamePhase].endTime)
        {
            gamePhase++;
            UpdatePhase(gamePhase);
        }

        enemy_normal_spawnTimer -= Time.deltaTime;
        enemy_speed_spawnTimer -= Time.deltaTime;
        enemy_giant_spawnTimer -= Time.deltaTime;
        if (enemy_normal_spawnTimer < 0f)
        {
            SpawnEnemy(enemy_normal_Prefab);
            enemy_normal_spawnTimer = phaseData.Phases[gamePhase].enemy_normal_spawntime;
        }
        if (enemy_speed_spawnTimer < 0f)
        {
            SpawnEnemy(enemy_speed_Prefab);
            enemy_speed_spawnTimer = phaseData.Phases[gamePhase].enemy_speed_spawntime;
        }
        if (enemy_giant_spawnTimer < 0f)
        {
            SpawnEnemy(enemy_giant_Prefab);
            enemy_giant_spawnTimer = phaseData.Phases[gamePhase].enemy_giant_spawntime;
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        // Choose whether to use secondary spawn area
        float secondarySpawnChance = 0.2f; // 20% chance to use secondary spawn area
        Vector2 spawnArea = UnityEngine.Random.value < secondarySpawnChance ? secondarySpawnArea : primarySpawnArea;

        // Generate random position in selected spawn area
        Vector3 position = new Vector3(
            UnityEngine.Random.Range(-spawnArea.x, spawnArea.x),
            UnityEngine.Random.Range(-spawnArea.y, spawnArea.y),
            0f
        );

        // Instantiate and initialize enemy
        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;
        newEnemy.GetComponent<EnemyController>().target = player;
    }

    private void UpdatePhase(int phase)
    {
        enemy_normal_spawnTimer = phaseData.Phases[phase].enemy_normal_spawntime;
        enemy_speed_spawnTimer = phaseData.Phases[phase].enemy_speed_spawntime;
        enemy_giant_spawnTimer = phaseData.Phases[phase].enemy_giant_spawntime;
    }

}
