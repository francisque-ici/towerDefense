using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private LevelInfo selectedLevel;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Khi UIManager chọn level, gọi GameManager
    public void OnLevelSelected(LevelInfo Level)
    {
        Debug.Log($"📢 GameManager: Nhận tín hiệu chọn Level {Level.LevelID}");
        selectedLevel = Level;
        PrepareForNewGame();
    }

    void PrepareForNewGame()
    {
        Debug.Log("🔸 GameManager: Chuẩn bị dữ liệu trước khi bắt đầu game...");

        // Gọi LevelLoader để tạo map
        GameObject loadedMap = LevelLoader.Instance.LoadLevel(selectedLevel.LevelID);
        if (loadedMap == null) return;

        // Cập nhật GridManager & PathFinder
        Vector3 basePos = loadedMap.transform.Find("Base").position;
        Vector3 spawnPos = loadedMap.transform.Find("Spawn").position;

        GridManager.Instance.UpdateGrid(loadedMap.transform.Find("PlaceZone").gameObject);
        PathFinder.Instance.NewPath(
            GridManager.Grid,
            new Vector2Int(Mathf.RoundToInt(spawnPos.x - GridManager.StartCorner.x), Mathf.RoundToInt(spawnPos.y - GridManager.StartCorner.y)),
            new Vector2Int(Mathf.RoundToInt(basePos.x - GridManager.StartCorner.x), Mathf.RoundToInt(basePos.y - GridManager.StartCorner.y))
        );

        GameplayData.Instance.BaseHealth = selectedLevel.BaseHealth;
        InputHandler.Instance.Enable();

        Debug.Log($"✅ GameManager: Level {selectedLevel.LevelID} đã load xong!");
    }
}
