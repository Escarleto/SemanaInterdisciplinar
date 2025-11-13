using UnityEngine;

public class CloseTutorial : MonoBehaviour
{
    public GameObject tutorial; // Assign in Inspector

    private void Update()
    {
        if (tutorial?.activeSelf == true && Input.GetButtonDown("Submit")) tutorial.SetActive(false);

    }
}