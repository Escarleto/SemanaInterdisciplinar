using UnityEngine;
using UnityEngine.UI;
public class PLEASEWOOORK : MonoBehaviour
{
    public Image targetImage;
    public Sprite[] icons;
    public KeyCode cycleKey = KeyCode.Space;

    private int currentIndex = 0;
    void Start()
    {
        if (icons.Length == 0 || targetImage == null)
        {
            Debug.LogError("Assign the targetImage and icons in the Inspector!");
            return;
        }
        targetImage.sprite = icons[currentIndex];
    }

   
    void Update()
    {
        if (Input.GetKeyDown(cycleKey))
        {
            currentIndex = (currentIndex + 1) % icons.Length;
            targetImage.sprite = icons[currentIndex];
        }
        
    }
}
