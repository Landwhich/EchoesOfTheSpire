using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFloat : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float floatSpeed = 0.5f;
    public float floatHeight = 0.25f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Rotate around the X-axis
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);

        // Float up and down on the Y-axis
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
