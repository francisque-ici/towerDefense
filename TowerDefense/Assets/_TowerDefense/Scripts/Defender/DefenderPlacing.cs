using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DefenderPlacing : MonoBehaviour
{
    public static DefenderPlacing Instance {get; private set;}
    public GameObject Defender;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
    }

    public void Place()
    {
        GameObject CurrentDefender = Instantiate(Defender, InputHandler.MousePosition, Quaternion.identity);
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("WTF");
            if (InputHandler.isInPlaceZone) Place();
        }
    }
}