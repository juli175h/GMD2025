using TMPro;
using UnityEngine;

public class SplatterProjectile : MonoBehaviour
{
    public Vector3 target; // Where it should land
    public float arcHeight = 0.5f;
    public GameObject splatterPrefab;
    public float duration;
    public float tickRate;
    public float damage;
    public float size;
    private Vector3 startPos;
    private float progress = 0f;
    public float travelTime = 1.5f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        progress += Time.deltaTime / travelTime;
        if (progress >= 1f)
        {
            Debug.Log("projectile reached goal");
            GameObject splatter = Instantiate(splatterPrefab, transform.position, Quaternion.identity);
            splatter.transform.localScale = Vector3.one * size;
            SplatterPool pool = splatter.GetComponentInChildren<SplatterPool>();
            if (pool != null)
            {
                pool.duration = duration;
                pool.tickRate = tickRate;
                pool.damage = damage;
            }
            Destroy(gameObject);
            return;
        }

        // Horizontal movement
        Vector3 horizontal = Vector3.Lerp(startPos, target, progress);

        // Arc height
        float height = Mathf.Sin(progress * Mathf.PI) * arcHeight;

        Vector3 newPos = new Vector3(horizontal.x, horizontal.y + height, horizontal.z);

        // Rotate to face movement direction
        Vector3 direction = (newPos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Move
        transform.position = newPos;
    }
}
