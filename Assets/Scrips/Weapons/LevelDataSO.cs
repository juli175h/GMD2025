using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class LevelDataSO<T> : ScriptableObject where T : LevelData
{
    public List<T> levels = new List<T>();
}

[Serializable]

public abstract class LevelData
{
    public string levelDescription;
}