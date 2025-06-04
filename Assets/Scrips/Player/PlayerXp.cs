using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;



public class PlayerXp : MonoBehaviour
{
    public int currentXp = 0;
    public int playerLevel = 0;
    public GameManagerScript gameManager;
    public GameObject levelUpCanvas;
    public WeaponManager weaponManager;
    public GameObject optionButtonPrefab;
    public Transform choicesContainer;
    public PlayerLevelSO xpToLevelupSO;
    public TextMeshProUGUI levelText;



    public Bar xpBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //xpTolevelUp = 100;
        xpBar.SetMaxValue(xpToLevelupSO.xpToLevelUp[playerLevel]);
        xpBar.SetValue(currentXp);

    }

 
    public void GainXp(int xpValue)
    {
        currentXp += xpValue;
        xpBar.SetValue(currentXp);
        if (currentXp >= xpToLevelupSO.xpToLevelUp[playerLevel])
            LevelUp();
        else
            SoundManager.PlaySound(SoundType.XP, 0.4f);

    }

    private void LevelUp()
    {
        SoundManager.PlaySound(SoundType.LEVEL_UP);
        playerLevel++;
        levelText.text = "Level " + (playerLevel+1);
        currentXp = 0;
        xpBar.SetMaxValue(xpToLevelupSO.xpToLevelUp[playerLevel]);
        xpBar.SetValue(currentXp);
        gameManager.Pause();
        levelUpCanvas.SetActive(true);

        ClearChoices();

        List<Weapon> availableOptions = new List<Weapon>();
        availableOptions.AddRange(weaponManager.GetUpgradeableWeapons());

        List<Weapon> selected = availableOptions.OrderBy(w => Random.value).Take(2).ToList();

        GameObject firstButton = null;

        foreach (Weapon weapon in selected)
        {
            GameObject buttonObj = Instantiate(optionButtonPrefab, choicesContainer);
            Button button = buttonObj.GetComponent<Button>();

            
            TextMeshProUGUI titleText = buttonObj.transform.Find("Title").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI descText = buttonObj.transform.Find("Description").GetComponent<TextMeshProUGUI>();
            if (titleText != null)
                titleText.text = weapon.weaponName + " Lv. " + (weapon.level + 1);
            if (descText != null)
                descText.text = weapon.GetNextLevelDescription();

            if (weapon.weaponImage != null)
            {
                Image weaponImage = buttonObj.transform.Find("WeaponImage").GetComponent<Image>(); 
                weaponImage.sprite = weapon.weaponImage;
            }
                
        

            button.onClick.AddListener(() => OnUpgradeSelected(weapon));

            if (firstButton == null)
            {
                firstButton = buttonObj;
            }
        }

        if (firstButton != null)
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
        }
    }


    public void OnUpgradeSelected(Weapon weapon)
    {
        SoundManager.PlaySound(SoundType.SELECT);
        weapon.LevelUp();

        ClearChoices();
        levelUpCanvas.SetActive(false);
        gameManager.Resume();
    }

    private void ClearChoices()
    {
        foreach (Transform child in choicesContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
