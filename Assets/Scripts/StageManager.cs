using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class StageManager : MonoBehaviour
{
    public GameObject Tile;
    public GameObject Pintor;
    private Animator Pintor_animator;
    private AudioSource Pintor_source;

    public AudioClip audio1;
    public AudioClip audio2;
    private GameManager GameManager => GameManager.Instance;
    public List<GameObject> Tiles = new List<GameObject>();

    void Start()
    {
        Pintor_animator = Pintor.GetComponent<Animator>();
        Pintor_source = Pintor.GetComponent<AudioSource>();
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                GameObject newTile = Instantiate(Tile, transform);
                newTile.transform.localPosition = new Vector3(i * 2.57f, 0f, j * 2.57f);
                newTile.GetComponent<Tile>().ChangeColor();

                Tiles.Add(newTile);
                //Pintor_animator.SetBool("Pintor_vira", true);
            }
        }
        //GameManager.Instance.RestartGameLoop();

       
    }

    public void RedrawTiles()
    {
        GameManager.RestartGameLoop();

        Color safeColor = GameManager.Instance.SafeColor;
        bool hasSafeTile = false;

        foreach (GameObject tile in Tiles)
        {
            tile.GetComponent<Tile>().ChangeColor();

            if (tile.GetComponent<Renderer>().material.color == safeColor)
            {
                hasSafeTile = true;
                Debug.Log("HasSafe Tiles");
            }
                tile.SetActive(true);
        }

        if (!hasSafeTile && Tiles.Count > 0)
        {
            Debug.Log("NOT Safe Tiles");
            int randomIndex = Random.Range(0, Tiles.Count);
            Tiles[randomIndex].GetComponent<Tile>().ChangeColor(safeColor);
        }

        Pintor_source.clip = audio1;
        Pintor_source.Play();
        Pintor_animator.SetBool("Pintor_Desenha", false);
        GameManager.PlayerRespawn();
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

        Pintor_source.clip = audio2;
        Pintor_source.Play();
        Pintor_animator.SetBool("Pintor_Desenha", true);
        GameManager.CreateTimer(GameManager.TimetoRedraw, RedrawTiles);
    }
}
