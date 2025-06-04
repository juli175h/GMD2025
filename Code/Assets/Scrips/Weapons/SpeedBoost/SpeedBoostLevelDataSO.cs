using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SpeedBoostLevelData", menuName = "Weapons/SpeedBoost Level Data")]
public class SpeedBoostLevelDataSO : LevelDataSO<SpeedBoostLevelData>
{
}

[System.Serializable]
public class SpeedBoostLevelData :LevelData
{
    public float cooldown;
    public float speedAmp;
    public float duration;
}