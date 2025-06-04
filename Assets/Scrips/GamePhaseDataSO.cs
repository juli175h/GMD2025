using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GamePhaseDataSO", menuName = "Scriptable Objects/GamePhaseDataSO")]
public class GamePhaseDataSO : ScriptableObject
{
    public List<GamePhaseData> Phases;
}
[System.Serializable]
public class GamePhaseData
{
    public float endTime;
    public float enemy_normal_spawntime;
    public float enemy_speed_spawntime;
    public float enemy_giant_spawntime;
    public float enemy_speed_cluster_spawntime;
}