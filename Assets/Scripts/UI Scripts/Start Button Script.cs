using UnityEngine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Newtonsoft.Json.Bson;
using UnityEngine.SceneManagement;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    private UIDocument _document;

    private Button _button;




    private void Awake()
    {
        _document = GetComponent<UIDocument>();

        _button = _document.rootVisualElement.Q("ButtonStart") as Button;
        _button.RegisterCallback<ClickEvent>(OnPlayGameClick);
    }

    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(OnPlayGameClick);
    }


    private void OnPlayGameClick(ClickEvent evt)
    {
        Debug.Log("Pressed Start");
        SceneManager.LoadScene("ColorPartyClone");
    }
}
