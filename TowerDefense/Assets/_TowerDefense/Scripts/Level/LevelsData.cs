using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsData", menuName = "Game/Levels Data")]
public class LevelsData : ScriptableObject
{
    public List<LevelInfo> Levels;
}

[System.Serializable]
public class LevelInfo
{
    public int LevelID;
    public GameObject LevelPrefab;
    public int BaseHealth;
    public List<WaveInfo> Waves;
}

[System.Serializable]

public class WaveInfo
{
    public int EPS;
}