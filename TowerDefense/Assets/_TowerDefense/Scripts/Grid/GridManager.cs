using System;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public static bool[,] Grid;
    public static Vector2 StartCorner;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
    }

    public void UpdateGrid(GameObject PlaceZone)
    {
        Debug.Log("OK");
        SpriteRenderer[] sprites = PlaceZone.GetComponentsInChildren<SpriteRenderer>();

        if (sprites.Length == 0) return;

        float minX = float.MaxValue, minY = float.MaxValue;
        float maxX = float.MinValue, maxY = float.MinValue;

        foreach (var sprite in sprites)
        {
            Vector3 pos = sprite.transform.position;
            float halfSizeX = sprite.bounds.size.x / 2; 
            float halfSizeY = sprite.bounds.size.y / 2;

            minX = Mathf.Min(minX, pos.x - halfSizeX + 0.5f);
            maxX = Mathf.Max(maxX, pos.x + halfSizeX + 0.5f);
            minY = Mathf.Min(minY, pos.y - halfSizeY + 0.5f);
            maxY = Mathf.Max(maxY, pos.y + halfSizeY + 0.5f);
        }

        StartCorner = new Vector2(minX, minY);
        Debug.Log(StartCorner);

        int width = Mathf.CeilToInt(maxX - minX);
        int height = Mathf.CeilToInt(maxY - minY);

        Grid = new bool[width, height];

        foreach (var sprite in sprites)
        {
            float halfSizeX = sprite.bounds.size.x / 2; 
            float halfSizeY = sprite.bounds.size.y / 2;

            for (int x = 0; x < sprite.bounds.size.x; x++)
            {
                for (int y = 0; y < sprite.bounds.size.y; y++)
                {
                    int gridX = Mathf.CeilToInt(sprite.transform.position.x - halfSizeX + x - minX);
                    int gridY = Mathf.CeilToInt(sprite.transform.position.y - halfSizeY + y - minY);
                    if (gridX >= 0 && gridX < width && gridY >= 0 && gridY < height)
                    {
                        Grid[gridX, gridY] = true;
                    }
                }
            }
        }
    }
}
