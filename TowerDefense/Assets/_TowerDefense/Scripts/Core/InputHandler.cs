using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance {get; private set;}
    public static bool isInPlaceZone = false;

    public int MousePositionFix = 2;
    
    private Vector2 MousePosition;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;
    }

    private Vector3 RoundVector3(Vector3 Vector)
    {
        return new Vector3(
            Mathf.Round((Vector.x + 0.5f) / MousePositionFix) * MousePositionFix - 0.5f,
            Mathf.Round((Vector.y + 0.5f) / MousePositionFix) * MousePositionFix - 0.5f,
            0
        );
    }

    private void UpdateMouse()
    {
        if (Input.touchCount > 0) // Mobile
        {
            MousePosition = RoundVector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));
        }
        else // PC
        {
            MousePosition = RoundVector3(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        Collider2D[] HitColliders = Physics2D.OverlapPointAll(MousePosition);
        
        isInPlaceZone = false;

        foreach (Collider2D Colliders in HitColliders)
        {
            if (Colliders.CompareTag("Placeable"))
            {
                Debug.Log("Is in place zone");
                return;
            }
        }
    }

    private void FixedUpdate()
    {
        UpdateMouse();
    }

}