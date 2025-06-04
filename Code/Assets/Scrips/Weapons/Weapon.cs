using UnityEngine;
using UnityEngine.UI;

public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public int level = 0;
    public int maxLevel = 5;
    public Sprite weaponImage;

    public void LevelUp()
    {
        if (level < maxLevel)
        {
            level++;
            ApplyLevelStats();
        }
    }

    public abstract void ApplyLevelStats();
    public abstract string GetNextLevelDescription();
}
