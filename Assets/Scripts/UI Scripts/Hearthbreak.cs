using UnityEngine;

public class Hearthbreak : MonoBehaviour
{
    public Animator animator;      // Drag your UI Image's Animator here
    public KeyCode triggerKey = KeyCode.Space; // Key to trigger animation
    public string animationClipName = "YourClipName"; // Name of the animation clip

    private bool isPlaying = false;

    void Update()
    {
        if (Input.GetKeyDown(triggerKey))
        {
            if (animator != null && !isPlaying)
            {
                animator.enabled = true;         
                animator.Play(animationClipName);  
                isPlaying = true;
            }
        }

        // Check if the animation has finished
        if (isPlaying)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            
            if (stateInfo.IsName(animationClipName) && stateInfo.normalizedTime >= 1f)
            {
                Hide();
                isPlaying = false;
            }
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}