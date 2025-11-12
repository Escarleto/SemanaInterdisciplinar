using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Newtonsoft.Json.Bson;
using UnityEngine.SceneManagement;


public class StartButtonScript : MonoBehaviour
{
    private UIDocument _document;

    private Button _buttonStart;

    private Button _buttonInfo;

    private VisualElement _tutorial;




    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _buttonStart = _document.rootVisualElement.Q("ButtonStart") as Button;
        _buttonInfo = _document.rootVisualElement.Q("ButtonInfo") as Button;

        _buttonStart.RegisterCallback<ClickEvent>(OnPlayGameClick);
        _buttonInfo.RegisterCallback<ClickEvent>(OnInfoClick);

        _tutorial = _document.rootVisualElement.Q("Tutorial");
    }

    private void OnDisable()
    {
        _buttonStart.UnregisterCallback<ClickEvent>(OnPlayGameClick);
        _buttonInfo.UnregisterCallback<ClickEvent>(OnInfoClick);
    }


    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("Pressed Start");
        SceneManager.LoadScene("ColorPartyClone");
    }
    private void OnInfoClick(ClickEvent evt)
    {
        Debug.Log("Pressed Info");
        _tutorial.style.display = DisplayStyle.Flex;
    }
}
