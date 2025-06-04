using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SplatterLevelData", menuName = "Weapons/Splatter Level Data")]
public class SplatterLevelDataSO : LevelDataSO<SplatterLevelData>
{
}

[System.Serializable]
public class SplatterLevelData : LevelData
{
    public float attackSpeed;
    public float duration;
    public float tickRate;
    public float damage;
    public float size;
}