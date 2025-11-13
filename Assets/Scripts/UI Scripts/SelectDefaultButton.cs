using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectDefaultButton : MonoBehaviour
{
    [SerializeField] private Button defaultButton; // assign in Inspector

    void Start()
    {
        // Make sure there's an EventSystem
        if (EventSystem.current == null)
        {
            new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }

        // Select this button when the scene starts
        EventSystem.current.SetSelectedGameObject(defaultButton.gameObject);
    }
}