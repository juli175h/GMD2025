using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PushBackLevelData", menuName = "Weapons/PushBack Level Data")]
public class PushBackLevelDataSO : ScriptableObject
{
    public List<PushBackLevelData> levels;
}

[System.Serializable]
public class PushBackLevelData
{
    public float cooldown;
    public float radius;
    public float duration;
    public float pushForce;
}
