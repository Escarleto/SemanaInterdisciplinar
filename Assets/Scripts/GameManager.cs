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
    public Plaquinha BoardManager;
    public Color SafeColor;
    public int Level = 0;
    public float TimetoAct = 5f;
    public float TimetoRedraw = 7f;

    public Dictionary<string, Color> ColorList = new Dictionary<string, Color>()
    {
        {"Red", Color.red},
        {"Green", Color.green},
        {"Blue", Color.blue}
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
            player.GetComponent<PlayerController>().PlayerID = Index;
            Players.Add(controller);
        }
    }

    public void StartGame()
    {
        if (Players.Count > 0)
        {
            foreach (PlayerController player in Players)
            {
                if (player.Ready)
                {
                    RestartGameLoop();
                }
            }
        }
    }

    public void RestartGameLoop()
    {
        Level += 1;
        HandleDifficulty();
        SafeColor = ColorList[
            new List<string>(ColorList.Keys)[UnityEngine.Random.Range(0, ColorList.Count)]
        ];
        BoardManager.UpdateColor();
        CreateTimer(TimetoAct, CanvasManager.HideUnsafeColors);
    }

    public void PlayerRespawn()
    {
        foreach (PlayerController player in Players)
        {
            player.HP_Handler();
        }
    }

    public void HandleDifficulty()
    {
        switch (Level)
        {
            case 1:
                TimetoAct = 7f;
                TimetoRedraw = 7f;
                break;
            case 3:
                ColorList.Add("Yellow", Color.yellow);
                break;
            case 4:
                TimetoAct = 5f;
                TimetoRedraw = 6f;
                break;
            case 5:
                ColorList.Add("Purple", new Color(0.5f, 0f, 0.5f));
                break;
            case 6:
                ColorList.Add("Orange", new Color(1f, 0.4f, 0f));
                break;
            case 7:
                TimetoAct = 6f;
                TimetoRedraw = 5f;
                break;
            case 10:
                TimetoAct = 3.5f;
                TimetoRedraw = 4f;
                break;
            case 13:
                TimetoAct = 2.5f;
                TimetoRedraw = 3f;
                break;
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
