using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    [Header (" Actions ")]
    public static Action onCarrotClick; // Action to be invoked when a carrot is clicked
    public static Action<Vector2> onCarrotClickPosition; // Action to be invoked with the position of the clicked carrot


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check if the left mouse button is pressed
        {
            Vector3 mousePosition = Input.mousePosition; // Get the mouse position in screen coordinates

            DebugLogger.Log("Mouse clicked at: " + mousePosition); // Log the mouse position

            ThrowRaycast(mousePosition); // Call the method to throw a raycast
        }
    }

    // Method to throw a raycast from the camera to the mouse position
    private void ThrowRaycast(Vector3 mousePosition)
    {
        if (Camera.main == null)
        {
            DebugLogger.LogError("No MainCamera found in the scene.");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null)
        {
            DebugLogger.Log("Hit object: " + hit.collider.name);
            onCarrotClick?.Invoke();
            onCarrotClickPosition?.Invoke(hit.point);
        }
        else
        {
            DebugLogger.Log("No object hit by the ray.");
        }
    }
}
