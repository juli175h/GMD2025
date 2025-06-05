using System.Collections;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SpeedBoostWeaponController : Weapon
{
    PlayerController player;
    float cooldown;
    [SerializeField] SpeedBoostLevelDataSO levelData;
    bool isUp = false;
    float timer = 0;
    float speedAmp;
    float duration;
    public InputAction SpeedBoostAction;
    public GameObject SpeedBoostUI;
    private Color disabledColor;
    private Color normalColor;
    private Color selectedColor;

    public override void ApplyLevelStats()
    {
        if(level > 0)
        {
            SpeedBoostAction.Enable();
            SpeedBoostAction.performed += OnSpeedBoostPressed;
            SpeedBoostUI.gameObject.SetActive(true);

        }
        else
        {
            SpeedBoostAction.Disable();
            SpeedBoostAction.performed -= OnSpeedBoostPressed;
            SpeedBoostUI.gameObject.SetActive(false);
        }

        
            var currentLevel = levelData.levels[level];
            duration = currentLevel.duration;
            cooldown = currentLevel.cooldown;
            speedAmp = currentLevel.speedAmp;
            isUp = true;
       
        

    }

    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
        ApplyLevelStats();
        ColorUtility.TryParseHtmlString("#94848484", out disabledColor);
        ColorUtility.TryParseHtmlString("#FFFFFF", out normalColor);
        ColorUtility.TryParseHtmlString("#E296CC", out selectedColor);


    }


    void Update()
    {
        if (!isUp)
        {
            if (timer < cooldown)
                {
                    timer += Time.deltaTime;
                    return;
                }
            timer = 0;
            SpeedBoostUI.gameObject.GetComponent<Image>().color = normalColor;
            isUp = true;
        }

    }
    private void OnSpeedBoostPressed(InputAction.CallbackContext context)
    {
        if (isUp)
        {
            StartCoroutine(SpeedBoost());
            isUp = false;
        }
    }

    IEnumerator SpeedBoost()
    {
        SpeedBoostUI.gameObject.GetComponent<Image>().color = selectedColor;
        float oldSpeed = player.movementSpeed;
        player.movementSpeed = oldSpeed * speedAmp;
        yield return new WaitForSeconds(duration);
        player.movementSpeed = oldSpeed;
        EventSystem.current.SetSelectedGameObject(null);
        SpeedBoostUI.gameObject.GetComponent<Image>().color = disabledColor;
    }

    public override string GetNextLevelDescription()
    {
        return levelData.levels[level + 1].levelDescription;
    }
}
