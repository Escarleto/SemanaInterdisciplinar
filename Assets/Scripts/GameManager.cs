using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private PlayerInputManager PlayerManager;

    public List<PlayerController> Players = new List<PlayerController>();
    public GameObject Tile;
    public StageManager CanvasManager;
    public Color SafeColor;

    public Dictionary<string, Color> ColorList = new Dictionary<string, Color>()
    {
        {"Red", Color.red},
        {"Green", Color.green},
        {"Blue", Color.blue},
        {"Yellow", Color.yellow},
        {"Orange", new Color(1f, 0.6f, 0f)},
        {"Purple", new Color(0.5f, 0f, 0.5f)}
    };

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        PlayerManager = GetComponent<PlayerInputManager>();
    }

    public void OnPlayerJoined(PlayerInput player)
    {
        Vector3[] SpawnPoints = new Vector3[]
        {
            new Vector3(1.19f, 2f, 8.88f),
            new Vector3(10.17f, 2f, 8.88f),
            new Vector3(1.19f, 2f, -6.11f),
            new Vector3(10.17f, 2f, -6.11f),
        };

        int Index = PlayerManager.playerCount - 1;
        player.transform.position = SpawnPoints[Index];

        var controller = player.GetComponent<PlayerController>();
        if (!Players.Contains(controller))
        {
            Players.Add(controller);
        }
    }

    public void NewSafeColor()
    {
        SafeColor = ColorList[
            new List<string>(ColorList.Keys)[UnityEngine.Random.Range(0, ColorList.Count)]
        ];

        CreateTimer(5f, CanvasManager.HideUnsafeColors);
    }

    public void PlayerRespawn()
    {
        foreach (PlayerController player in Players)
        {
            player.HP_Handler();
        }
    }

    public void CreateTimer(float duration, Action callback)
    {
        StartCoroutine(TimerCoroutine(duration, callback));
    }

    private IEnumerator TimerCoroutine(float duration, Action callback)
    {
        yield return new WaitForSeconds(duration);
        callback?.Invoke();
    }
}
