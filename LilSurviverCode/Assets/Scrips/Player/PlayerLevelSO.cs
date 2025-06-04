using NUnit.Framework;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLevelSO", menuName = "Scriptable Objects/PlayerLevelSO")]
public class PlayerLevelSO : ScriptableObject
{
    public List<int> xpToLevelUp;
}
