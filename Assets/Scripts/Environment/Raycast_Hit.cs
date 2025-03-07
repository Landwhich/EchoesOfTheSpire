using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast_Hit : MonoBehaviour
{
    private MeshRenderer wallRenderer;
    // Start is called before the first frame update
    void Start()
    {
        wallRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeColor(Color newColor)
    {
        if (wallRenderer != null && wallRenderer.material != null)
        {
            wallRenderer.material.color = newColor;
        }
    }
}
