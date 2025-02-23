using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance { get; private set; }

    [Header("Levels Data")]
    public LevelsData LevelDatabase;

    private GameObject currentLevel;
    private int currentLevelIndex = -1;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public GameObject LoadLevel(int levelID)
    {
        LevelInfo levelInfo = LevelDatabase.Levels.Find(l => l.LevelID == levelID);

        if (levelInfo == null || levelInfo.LevelPrefab == null)
        {
            Debug.LogError($"Level ID {levelID} không hợp lệ hoặc chưa có prefab!");
            return null;
        }

        // Xóa level cũ trước khi load level mới
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }

        // Load level mới
        currentLevel = Instantiate(levelInfo.LevelPrefab, Vector3.zero, Quaternion.identity);
        currentLevelIndex = levelID;

        return currentLevel;
    }

    public void ReloadLevel()
    {
        if (currentLevelIndex != -1)
        {
            LoadLevel(currentLevelIndex);
        }
    }

    public void LoadNextLevel()
    {
        int nextIndex = LevelDatabase.Levels.FindIndex(l => l.LevelID == currentLevelIndex) + 1;

        if (nextIndex < LevelDatabase.Levels.Count)
        {
            LoadLevel(LevelDatabase.Levels[nextIndex].LevelID);
        }
    }

    public void LoadPreviousLevel()
    {
        int prevIndex = LevelDatabase.Levels.FindIndex(l => l.LevelID == currentLevelIndex) - 1;

        if (prevIndex >= 0)
        {
            LoadLevel(LevelDatabase.Levels[prevIndex].LevelID);
        }
    }
}
