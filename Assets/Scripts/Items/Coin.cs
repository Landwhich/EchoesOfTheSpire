using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCoin : MonoBehaviour
{
    public Transform player;  
    public float floatHeight = 0.5f;
    public float floatSpeed = 1f;
    public float rotationSpeed = 50f;

    private Vector3 originalPosition;
    // private bool isFloatingUp = true;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        FloatEffect();

        Rotate();
    }

    void FloatEffect()
    {
        float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight + originalPosition.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void Rotate()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
