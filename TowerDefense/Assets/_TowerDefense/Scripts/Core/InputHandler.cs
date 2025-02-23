using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance {get; private set;}
    public static bool isInPlaceZone = false;
    public static Vector3 MousePosition;
    public int MousePositionFix = 2;
    public GameObject MouseTarget;
    private bool isEnabled;
    
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
        MouseTarget.transform.position = MousePosition + new Vector3(0, 0, -5);

        foreach (Collider2D Colliders in HitColliders)
        {
            if (Colliders.CompareTag("Placeable"))
            {
                isInPlaceZone = true;
                return;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isEnabled) UpdateMouse();
    }

    public void Enable()
    {
        isEnabled = true;
    }

    public void Disable()
    {
        isEnabled = false;
    }

}