using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PushBackWeaponController : Weapon
{
    PlayerController player;
    float cooldown;
    [SerializeField] bool isUp = false;
    float timer = 0;
    float radius;
    float pushForce;
    float duration;
    [SerializeField] PushBackLevelDataSO levelData;
    public InputAction PushBackAction;
    public Button PushBackUI;

    public override void ApplyLevelStats()
    {
        if (level > 0)
        {
            PushBackAction.Enable();
            PushBackAction.performed += OnPushBackPressed;
            PushBackUI.gameObject.SetActive(true);
        }
        else
        {
            PushBackAction.Disable();
            PushBackAction.performed -= OnPushBackPressed;
            PushBackUI.gameObject.SetActive(false);
        }

        var currentLevel = levelData.levels[level];
        duration = currentLevel.duration;
        cooldown = currentLevel.cooldown;
        radius = currentLevel.radius;
        pushForce = currentLevel.pushForce;
        isUp = true;
    }

    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
        ApplyLevelStats();
    }

    //void Update()
    //{
    //    if (!isUp)
    //    {
    //        if (timer < cooldown)
    //        {
    //            timer += Time.deltaTime;
    //            return;
    //        }
    //        timer = 0;
    //        PushBackUI.interactable = true;
    //        isUp = true;
    //    }
    //}

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Vector2 origin = transform.position;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                Vector2 dir = ((Vector2)enemy.transform.position - origin).normalized;
                Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector2.zero;
                    rb.AddForce(dir * 20f, ForceMode2D.Impulse);
                }
            }

            Debug.Log("Attempted brute-force push.");
        }
    }


    private void OnPushBackPressed(InputAction.CallbackContext context)
    {
        if (isUp)
        {
            StartCoroutine(PushBack());
            isUp = false;
        }
    }

    IEnumerator PushBack()
    {
        EventSystem.current.SetSelectedGameObject(PushBackUI.gameObject);

        Vector2 origin = player.transform.position;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Vector2 enemyPos = enemy.transform.position;
            Vector2 direction = enemyPos - origin;
            float distance = direction.magnitude;

            if (distance <= radius)
            {
                Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
                EnemyController enemyScript = enemy.GetComponent<EnemyController>();

                if (rb != null && enemyScript != null)
                {
                    Vector2 pushDir = direction.normalized;
                    rb.AddForce(pushDir * pushForce, ForceMode2D.Impulse);

                    enemyScript.isPushedBack = true;
                    enemyScript.pushBackTimer = duration;

                    Debug.Log($"Pushed enemy {enemy.name}");
                }
            }
        }

        yield return new WaitForSeconds(duration);
        EventSystem.current.SetSelectedGameObject(null);
        PushBackUI.interactable = false;
    }


    private void OnDrawGizmosSelected()
    {
        if (player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius); // Again, use transform.position directly
        }
    }

    public override string GetNextLevelDescription()
    {
        throw new System.NotImplementedException();
    }
}
