using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class StageManager : MonoBehaviour
{
    public GameObject Tile;
    private GameManager GameManager => GameManager.Instance;
    public List<GameObject> Tiles = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                GameObject newTile = Instantiate(Tile, transform);
                newTile.transform.localPosition = new Vector3(i * 2.57f, 0f, j * 2.57f);
                newTile.GetComponent<Tile>().ChangeColor();

                Tiles.Add(newTile);
            }
        }
        //GameManager.Instance.RestartGameLoop();
    }

    public void RedrawTiles()
    {
        Color safeColor = GameManager.Instance.SafeColor;
        bool hasSafeTile = false;

        foreach (GameObject tile in Tiles)
        {
            tile.GetComponent<Tile>().ChangeColor();
            tile.SetActive(true);

            if (tile.GetComponent<Renderer>().material.color == safeColor)
            {
                hasSafeTile = true;
            }
        }

        if (!hasSafeTile && Tiles.Count > 0)
        {
            int randomIndex = Random.Range(0, Tiles.Count);
            Tiles[randomIndex].GetComponent<Tile>().ChangeColor(safeColor);
        }

        GameManager.PlayerRespawn();
        GameManager.RestartGameLoop();
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

        GameManager.CreateTimer(GameManager.TimetoRedraw, RedrawTiles);
    }
}
