using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BulletLevelData", menuName = "Weapons/Bullet Level Data")]
public class BulletLevelDataSO : LevelDataSO<BulletLevelData>
{
}

[System.Serializable]
public class BulletLevelData : LevelData
{
    public float attackSpeed;
    public float Damage;
}