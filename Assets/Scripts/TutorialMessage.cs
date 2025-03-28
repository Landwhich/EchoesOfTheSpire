using UnityEngine;
using UnityEngine.UI;

public class TutorialMessage : MonoBehaviour
{
    public GameObject tutorialText; // Drag your UI Text here

    void Start()
    {
        tutorialText.gameObject.SetActive(true); // Ensure text is visible at start
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect left mouse click
        {
            tutorialText.gameObject.SetActive(false); // Hide text on click
        }
    }
}
