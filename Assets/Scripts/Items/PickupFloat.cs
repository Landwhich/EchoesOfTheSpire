using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFloat : MonoBehaviour
{
    public float floatSpeed = 0.5f;
    public float floatHeight = 0.25f;
    public int healthAmount = 25;  // The amount of health the player gains

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Float up and down on the Y-axis
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // Detect collision with the player
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Make sure the player has the "Player" tag
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Heal the player
                playerHealth.Heal(healthAmount);
                Debug.Log("Health Picked Up! Player health is now: " + playerHealth.curHealth);
            }

            // Destroy the health pickup item after pickup
            Destroy(gameObject);
        }
    }
}
