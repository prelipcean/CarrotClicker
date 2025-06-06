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
        if (Input.touchCount > 0) // Check if there are any touches on the screen
        {
            ManageTouches(); // Call the method to manage touches
        }
        
        /*
        if (Input.GetMouseButtonDown(0)) // Check if the left mouse button is pressed
        {
            Vector3 mousePosition = Input.mousePosition; // Get the mouse position in screen coordinates

            DebugLogger.Log("Mouse clicked at: " + mousePosition); // Log the mouse position

            ThrowRaycast(mousePosition); // Call the method to throw a raycast
        }
        */
    }

    private void ManageTouches()
    {
        for (int i = 0; i < Input.touchCount; i++) // Loop through all active touches
        {
            Touch touch = Input.GetTouch(i);

            if (touch.phase == TouchPhase.Began)
            { 
                ThrowRaycastTouch(touch.position); // Call the method to throw a raycast with the touch position
            }
        }


            // Touch touch = Input.GetTouch(0); // Get the first touch

            // if (touch.phase == TouchPhase.Began) // Check if the touch just began
            // {
            //     Vector3 touchPosition = touch.position; // Get the position of the touch in screen coordinates

            //     DebugLogger.Log("Touch began at: " + touchPosition); // Log the touch position

            //     ThrowRaycast(touchPosition); // Call the method to throw a raycast
            // }
    }

    private void ThrowRaycastTouch(Vector2 touchPosition)
    {
        if (Camera.main == null)
        {
            DebugLogger.LogError("No MainCamera found in the scene.");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
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
