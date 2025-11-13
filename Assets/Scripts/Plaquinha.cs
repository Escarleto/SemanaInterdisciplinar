using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Plaquinha : MonoBehaviour
{
    public GameObject SplashL;
    public GameObject SplashR;
    public GameObject SplashC;
    private TextMeshPro textmesh;
    public Color safecolor;

    void Start()
    {
        SplashL.SetActive(false);
        SplashR.SetActive(false);
        SplashC.SetActive(true);
    }

    public void UpdateColor()
    {
        Color SafeColor = GameManager.Instance.SafeColor;

        if (GameManager.Instance.Level < 9)
        {
            SplashL.SetActive(false);
            SplashR.SetActive(false);
            SplashC.GetComponent<SpriteRenderer>().color = SafeColor;
        }
        else
        {
            SplashL.SetActive(true);
            SplashR.SetActive(true);
            SplashC.SetActive(false);

            Color R_Color = SplashR.GetComponent<SpriteRenderer>().color;
            Color L_Color = SplashL.GetComponent<SpriteRenderer>().color;

            if (SafeColor == Color.red)
            {
                L_Color = Color.magenta;
                R_Color = Color.yellow;
            }
            else if (SafeColor == Color.green)
            {
                L_Color = Color.blue;
                R_Color = Color.yellow;
            }
            else if (SafeColor == Color.blue)
            {
                L_Color = Color.magenta;
                R_Color = Color.cyan;
            }
            else if (SafeColor == Color.yellow)
            {
                SplashL.SetActive(false);
                SplashR.SetActive(false);
                SplashC.SetActive(true);
                SplashC.GetComponent<SpriteRenderer>().color = SafeColor;
            }
            else if (SafeColor == new Color(0.5f, 0f, 0.5f))
            {
                L_Color = Color.blue;
                R_Color = Color.red;
            }
            else if (SafeColor == new Color(1f, 0.4f, 0f))
            {
                L_Color = Color.yellow;
                R_Color = Color.red;
            }

            SplashR.GetComponent<SpriteRenderer>().color = R_Color;
            SplashL.GetComponent<SpriteRenderer>().color = L_Color;
        }
    }
}
