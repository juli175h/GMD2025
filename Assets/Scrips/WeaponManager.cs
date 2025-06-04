using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> allWeapons;

    void Awake()
    {
        // Optionally auto-fill from child objects
        allWeapons = GetComponentsInChildren<Weapon>(true).ToList();
    }

    public List<Weapon> GetUpgradeableWeapons()
    {
        return allWeapons.Where(w =>  w.level < w.maxLevel).ToList();
    }

 
}
