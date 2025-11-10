using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CanvasManager : MonoBehaviour
{
    private GameObject Tile;

    void Start()
    {
        Tile = GameManager.Instance.Tile;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                GameObject newTile = Instantiate(Tile, transform);
                newTile.transform.localPosition = new Vector3(i * 2.56f, 0f, j * 2.56f);
                newTile.GetComponent<Tile>().ChangeColor();
            }
        }

        GameManager.Instance.NewSafeColor();
        GameManager.Instance.CreateTimer(7f, HideUnsafeColors);
    }

    public void ReDrawTiles()
    {
        foreach (Transform Tile in transform)
        {
            Tile.GetComponent<Tile>().ChangeColor();
            Tile.gameObject.SetActive(true);
        }

        GameManager.Instance.NewSafeColor();
        GameManager.Instance.CreateTimer(7f, HideUnsafeColors);
    }

    public void HideUnsafeColors()
    { 
        Color safeColor = GameManager.Instance.SafeColor;
        foreach (Transform Tile in transform)
        {
            Renderer Mat = Tile.GetComponent<Renderer>();
            if (Mat.material.color != safeColor)
            {
                Tile.gameObject.SetActive(false);
            }
        }

        GameManager.Instance.CreateTimer(5f, ReDrawTiles);
    }
}
