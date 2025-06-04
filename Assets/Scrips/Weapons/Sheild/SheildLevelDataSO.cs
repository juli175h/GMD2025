using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SheildLevelData", menuName = "Weapons/Sheild Level Data")]
public class SheildLevelDataSO : LevelDataSO<SheildLevelData>
{
}

[System.Serializable]
public class SheildLevelData : LevelData
{
    public float attackSpeed;
    public float duration;
    public float damage;
}