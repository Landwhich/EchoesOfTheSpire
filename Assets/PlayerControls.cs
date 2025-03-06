using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed = 5f;  // Speed of movement
    public float floatHeight = 0.05f;  // Hover range
    public float floatSpeed = 1f;  // Speed of up/down movement (slow oscillation)
    public float moveSpeed = 5f;  // Speed of movement along X and Z axes

    public Transform playerBody;  // Reference to the player's body (for rotation, if needed)
    private CharacterController controller;  // Character controller for movement

    private Vector3 originalPosition;  // Store the original position for floating effect

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalPosition = transform.position;
    }

    void Update()
    {
        // Player Movement with WASD
        float moveX = Input.GetAxis("Horizontal") * moveSpeed; // Left/Right (A/D)
        float moveZ = Input.GetAxis("Vertical") * moveSpeed; // Forward/Backward (W/S)

        // Calculate movement direction
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Apply movement
        controller.Move(move * Time.deltaTime);  // Apply movement with CharacterController

        // Floating effect
        float newY = originalPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // Update the position of the cube (only the Y axis, leave X and Z unchanged)
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
