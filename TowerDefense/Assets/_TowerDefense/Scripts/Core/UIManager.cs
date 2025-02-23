using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Elements")]
    public Text HealthDisplay;
    public Transform ButtonContainer; // Nơi chứa button
    public GameObject levelSelectionUI;
    public GameObject ButtonPrefab; // Prefab của button

    [Header("Game Data")]
    public LevelsData levelsData; // ScriptableObject chứa levels

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;

        GenerateLevelButtons();
    }

    public void UpdateHealthDisplay(int newHealth)
    {
        HealthDisplay.text = newHealth.ToString();
    }

    public void GenerateLevelButtons()
    {
        if (levelsData == null || levelsData.Levels == null)
        {
            Debug.LogError("LevelsData chưa được gán hoặc không có level nào!");
            return;
        }

        foreach (Transform child in ButtonContainer)
        {
            Destroy(child.gameObject); // Xóa các button cũ trước khi tạo mới
        }

        foreach (var level in levelsData.Levels)
        {
            GameObject newButton = Instantiate(ButtonPrefab, ButtonContainer);
            Button buttonComponent = newButton.GetComponent<Button>();

            Text buttonText = newButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = $"Level {level.LevelID}";
            }

            // Tạo biến cục bộ để tránh lỗi lambda closure
            buttonComponent.onClick.AddListener(() => OnLevelButtonClicked(level));
        }
    }

    private void OnLevelButtonClicked(LevelInfo level)
    {
        Debug.Log($"Button Level {level.LevelID} được nhấn!");

        // Gửi tín hiệu đến GameManager
        GameManager.Instance.OnLevelSelected(level);

        // Ẩn UI chọn level sau khi chọn
        HideLevelSelectionUI();
    }

    public void HideLevelSelectionUI()
    {
        if (levelSelectionUI != null)
        {
            levelSelectionUI.SetActive(false);
        }
        else
        {
            Debug.LogError("Level Selection UI chưa được gán trong Inspector!");
        }
    }

}
