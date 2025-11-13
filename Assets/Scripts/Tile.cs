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

    public void ChangeColor(Color? forcedColor = null)
    {
        if (forcedColor.HasValue)
        {
            Mat.color = forcedColor.Value;
            return;
        }

        Dictionary<string, Color> colorList = GameManager.Instance.ColorList;
        List<string> keys = new List<string>(colorList.Keys);
        Mat.color = colorList[keys[Random.Range(0, keys.Count)]];
    }

}
