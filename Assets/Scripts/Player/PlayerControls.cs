using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float floatHeight = 0.05f;
    public float floatSpeed = 1f;
    public float moveSpeed = 7f;
    public float sprintSpeed = 12f;
    public float mouseSensitivity = 700f;

    public float sprintDuration = 4f;
    public float sprintCooldown = 3f;

    private bool isSprinting = false;
    private bool isCooldown = false;
    private float sprintTimer = 0f;
    private float cooldownTimer = 0f;

    public Transform playerBody;
    private float xRotation = 0f;

    public Camera playerCamera;
    private CharacterController controller;
    private float originalY;

    public AudioSource audioSource;
    public AudioClip shotSound;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalY = transform.position.y;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        playerBody.Rotate(Vector3.up * mouseX);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 moveDirection = (transform.right * moveX + transform.forward * moveZ).normalized;

        // Sprint logic
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSprinting && !isCooldown && moveDirection.magnitude > 0)
        {
            isSprinting = true;
            sprintTimer = sprintDuration;
        }

        if (isSprinting)
        {
            sprintTimer -= Time.deltaTime;
            if (sprintTimer <= 0f)
            {
                isSprinting = false;
                isCooldown = true;
                cooldownTimer = sprintCooldown;
            }
        }

        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isCooldown = false;
            }
        }

        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;
        controller.Move(moveDirection * currentSpeed * Time.deltaTime);

        // Floating effect on Y
        Vector3 pos = transform.position;
        pos.y = originalY + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = pos;

        // Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            ShootRay();
        }
    }

    void ShootRay()
    {
        if (audioSource == null || shotSound == null)
        {
            Debug.LogError("Missing AudioSource or ShotSound.");
            return;
        }

        audioSource.volume = 0.05f;
        audioSource.PlayOneShot(shotSound);

        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Ray hit: " + hit.collider.name);
        }
    }
}
