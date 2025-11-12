using UnityEngine;

public class Hearthbreak : MonoBehaviour
{
    public Animator animator;      // Drag your UI Image's Animator here
    public KeyCode triggerKey = KeyCode.Space; // Key to trigger animation

    void Update()
    {
        if (Input.GetKeyDown(triggerKey))
        {
            if (animator != null)
            {
                animator.enabled = true;         // Enable Animator
                animator.Play("YourClipName");  // Optional: start a specific clip
            }
        }
    }
}