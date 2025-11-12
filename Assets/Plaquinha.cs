using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Plaquinha : MonoBehaviour
{
    public GameObject texto;
    public GameObject imagem_do_quadro;
    private TextMeshPro textmesh;
    public Color safecolor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color SafeColor = GameManager.Instance.SafeColor;

        safecolor = SafeColor;

        texto.GetComponent<TextMeshProUGUI>().color = SafeColor;

    }
}
