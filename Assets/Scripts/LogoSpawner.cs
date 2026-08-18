using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LogoSpawner : MonoBehaviour
{

    public GameObject LogoObject;  // Store of the logo object to be spawned.

    // Update runs once every frame.
    private void Update()
    {
        // If we detect a left mouse click, spawn a logo at the mouse position.
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            SpawnLogoAtMousePosition();
        }
    }
    
    // Spawn a single logo at the mouse's current position.
    private void SpawnLogoAtMousePosition()
    {
        // Get the X and Y positions of the mouse.
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        float mouseX = worldMousePos.x;
        float mouseY = worldMousePos.y;
        
        // Create a copy of the object, then store it in a variable.
        GameObject logoCopy = Instantiate(LogoObject);
        
        //////////////////////////////////////////////////////////////////////////
        // TODO: It's your turn to write the final line of code!                //
        // Write code below to place the logoCopy object at the mouse position. //
        // The game object is stored in the `logoCopy` variable, and the mouse  //
        // X and Y are stored in `mouseX` and `mouseY`.                         //
        //////////////////////////////////////////////////////////////////////////

        

    }
}
