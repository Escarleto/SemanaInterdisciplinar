using NUnit.Framework.Internal;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Material Mat;

    void Awake()
    {
        Mat = GetComponent<Renderer>().material;
    }

    public void ChangeColor()
    {
        Color randomColor = Random.ColorHSV();

        Mat.color = randomColor;
    }
}
