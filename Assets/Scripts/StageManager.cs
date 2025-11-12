using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class StageManager : MonoBehaviour
{
    public GameObject Tile;
    public List<GameObject> Tiles = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                GameObject newTile = Instantiate(Tile, transform);
                newTile.transform.localPosition = new Vector3(i * 2.56f, 0f, j * 2.56f);
                newTile.GetComponent<Tile>().ChangeColor();

                Tiles.Add(newTile);
            }
        }
        GameManager.Instance.NewSafeColor();
    }

    public void RedrawTiles()
    {
        foreach (GameObject tile in Tiles)
        {
            tile.GetComponent<Tile>().ChangeColor();
            tile.SetActive(true);
        }

        GameManager.Instance.PlayerRespawn();
        GameManager.Instance.NewSafeColor();
    }

    public void HideUnsafeColors()
    {
        foreach (GameObject tile in Tiles)
        {
            if (tile.GetComponent<Renderer>().material.color != GameManager.Instance.SafeColor)
            {
                tile.SetActive(false);
            }
        }

        GameManager.Instance.CreateTimer(4f, RedrawTiles);
    }
}
