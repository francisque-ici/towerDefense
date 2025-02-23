using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public static PathFinder Instance {get; private set;}
    private List<Vector2Int> Path;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
    }

    public void NewPath(bool[,] Grid, Vector2Int Start, Vector2Int End)
    {
        int rows = Grid.GetLength(0);
        int cols = Grid.GetLength(1);

        Queue<Vector2Int> queue = new Queue<Vector2Int>(); // Hàng đợi BFS
        Dictionary<Vector2Int, Vector2Int> CameFrom = new Dictionary<Vector2Int, Vector2Int>(); // Lưu vết đường đi
        Path = new List<Vector2Int>(); // Đường đi cuối cùng

        // Hướng di chuyển: Trên, Dưới, Trái, Phải
        Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

        queue.Enqueue(Start);
        CameFrom[Start] = Start; // Đánh dấu điểm bắt đầu

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            if (current == End) // Nếu tìm thấy đích, kết thúc
            {
                break;
            }

            foreach (var dir in directions)
            {
                Vector2Int next = current + dir;
                if (IsValid(next, rows, cols) && !Grid[next.x, next.y] && !CameFrom.ContainsKey(next))
                {
                    queue.Enqueue(next);
                    CameFrom[next] = current; // Lưu vết đường đi
                }
            }
        }

        // Duyệt ngược lại để lấy đường đi từ end -> start
        if (CameFrom.ContainsKey(End))
        {
            Vector2Int Step = End;
            while (Step != Start)
            {
                Path.Add(Step);
                Step = CameFrom[Step];
            }
            Path.Add(Start);
            Path.Reverse(); // Đảo ngược để có thứ tự start -> end
        }
    }

    private bool IsValid(Vector2Int pos, int rows, int cols)
    {
        return pos.x >= 0 && pos.x < rows && pos.y >= 0 && pos.y < cols;
    }

    public List<Vector2Int> GetPath()
    {
        return Path;
    }

}
