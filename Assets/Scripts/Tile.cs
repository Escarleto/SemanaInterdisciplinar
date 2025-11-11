using NUnit.Framework.Internal;
using System.Collections.Generic;
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
        Dictionary<string, Color> colorList = GameManager.Instance.ColorList;
        Color randomColor = colorList[
            new List<string>(colorList.Keys)[Random.Range(0, colorList.Count)]
        ];

        Mat.color = randomColor;
    }
}
