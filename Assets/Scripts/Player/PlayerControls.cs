using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed = 5f;  // Speed of movement
    public float floatHeight = 0.05f;  // Hover range
    public float floatSpeed = 1f;  // Speed of up/down movement (slow oscillation)
    public float moveSpeed = 5f;  // Speed of movement along X and Z axes
    public float mouseSensitivity = 700f;

    public Transform playerBody;  // Reference to the player's body (for rotation, if needed)

    private float xRotation = 0f;

    public Camera playerCamera;

    private CharacterController controller;  // Character controller for movement

    private Vector3 originalPosition;  // Store the original position for floating effect

    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip shotSound;  // Audio clip to be played when the ray is shot
     
    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalPosition = transform.position;
        Cursor.lockState = CursorLockMode.Locked;


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Player Movement with WASD
        float pitch = mouseSensitivity * Input.GetAxis("Mouse Y") * Time.deltaTime;
        float yaw = mouseSensitivity * Input.GetAxis("Mouse X") * Time.deltaTime;

        float moveX = Input.GetAxis("Horizontal") * moveSpeed; // Left/Right (A/D)
        float moveZ = Input.GetAxis("Vertical") * moveSpeed; // Forward/Backward (W/S)

        // Calculate movement and rotation direction
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // apply rotation
        playerBody.Rotate(Vector3.up * yaw);
        xRotation -= pitch;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Apply movement
        controller.Move(move * Time.deltaTime);  // Apply movement with CharacterController

        // Floating effect
        float newY = originalPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // Update the position of the cube (only the Y axis, leave X and Z unchanged)
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        if (Input.GetButtonDown("Fire1"))  // Default is left mouse click
        {
            ShootRay();
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        playerBody.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Prevent flipping the camera
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void ShootRay()
    {
        audioSource.volume = 0.05f;
        // Log to ensure the function is being called
        Debug.Log("Ray fired!");

        // Check if AudioSource and AudioClip are set
        if (audioSource == null)
        {
            Debug.LogError("AudioSource is not assigned!");
            return;
        }

        if (shotSound == null)
        {
            Debug.LogError("Shot sound is not assigned!");
            return;
        }

        // Play the shot sound
        audioSource.PlayOneShot(shotSound);  // Play the sound once

        // Raycast logic
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Ray hit: " + hit.collider.name);
        }
    }
}
