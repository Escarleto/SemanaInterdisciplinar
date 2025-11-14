using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartButtonScriptCanvas : MonoBehaviour
{
    public Button buttonStart;
    public Button buttonInfo;
    public GameObject tutorial;
    public Button buttonTutorial;
    public Button emptyButton;

    private void Awake()
    {
        buttonStart.onClick.AddListener(OnPlayGameClick);
        buttonInfo.onClick.AddListener(OnInfoClick);
        buttonTutorial.onClick.AddListener(OnTutorialClick);

        tutorial.SetActive(false);
    }




    private void OnPlayGameClick()
    {
        Debug.Log("Pressed Start");
        SceneManager.LoadScene("ColorPartyClone");
    }

    private void OnInfoClick()
    {
        tutorial.SetActive(true);
        EventSystem.current.SetSelectedGameObject(buttonTutorial.gameObject);
    }

    private void OnTutorialClick()
    {
       tutorial.SetActive(false);
        EventSystem.current.SetSelectedGameObject(emptyButton.gameObject);
    }
}

