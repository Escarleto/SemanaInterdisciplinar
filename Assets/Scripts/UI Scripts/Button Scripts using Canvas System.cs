using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartButtonScriptCanvas : MonoBehaviour
{
    public Button buttonStart;   // assign in Inspector
    public Button buttonInfo;    // assign in Inspector
    public GameObject tutorial;  // assign in Inspector

    private bool ignoreInputThisFrame;

    private void Awake()
    {
        // Register listeners for clicks
        buttonStart.onClick.AddListener(OnPlayGameClick);
        buttonInfo.onClick.AddListener(OnInfoClick);

        // Make sure tutorial is hidden initially
        if (tutorial != null)
            tutorial.SetActive(false);
    }

    private void OnDestroy()
    {
        // Clean up listeners
        buttonStart.onClick.RemoveListener(OnPlayGameClick);
        buttonInfo.onClick.RemoveListener(OnInfoClick);
    }

    private void Update()
{
    if (tutorial != null && tutorial.activeSelf)
    {
        if (ignoreInputThisFrame)
            return;

        if (Input.GetMouseButtonDown(0))
            tutorial.SetActive(false);
    }
}


    private void OnPlayGameClick()
    {
        Debug.Log("Pressed Start");
        SceneManager.LoadScene("ColorPartyClone");
    }

    private void OnInfoClick()
    {
        if (tutorial != null)
        {
            tutorial.SetActive(true);

            if (EventSystem.current != null)
                EventSystem.current.SetSelectedGameObject(null);

            ignoreInputThisFrame = true;
            StartCoroutine(ResetIgnoreInput());
        }
    }
    private System.Collections.IEnumerator ResetIgnoreInput()
    {
        yield return null; // wait one frame
        ignoreInputThisFrame = false;
    }
}