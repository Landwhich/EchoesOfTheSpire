using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast_Hit : MonoBehaviour
{
    private Renderer wallRenderer;
    // Start is called before the first frame update
    void Start()
    {
        wallRenderer = GetComponent<Renderer>();
    }

    public void ChangeColour(Color newColour)
    {
        if (wallRenderer != null)
        {
            wallRenderer.material.color = newColour;
        }
    }
}
