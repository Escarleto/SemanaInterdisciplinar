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
        var ColorList = GameManager.Instance.ColorList;
        Color randomColor = ColorList[
        new List<string>(ColorList.Keys)[Random.Range(0, ColorList.Count)]];

        Mat.color = randomColor;
    }
}
