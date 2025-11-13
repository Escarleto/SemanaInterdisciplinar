using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public Button RedButton;
    public Button YellowButton;
    public Button GreenButton;
    public Button PurpleButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RedButton.onClick.AddListener(RedClick); 
    }

    void RedClick()
    {

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
