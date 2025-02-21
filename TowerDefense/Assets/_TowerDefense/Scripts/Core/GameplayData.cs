using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayData : MonoBehaviour
{
    public static GameplayData Instance;

    public int Point = 0;
    public int Money = 0;
    public int BaseHealth = 0;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
    }
}