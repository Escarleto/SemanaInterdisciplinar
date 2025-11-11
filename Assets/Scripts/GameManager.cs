using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private PlayerInputManager PlayerManager;

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
        Debug.Log($"Player Joined: {player.playerIndex}");
        Vector3[] SpawnPoints = new Vector3[]
        {
            new Vector3(2.56f, 1f, 2.56f),
            new Vector3(7.68f, 1f, 2.56f),
        };
        int Index = PlayerManager.playerCount - 1;
        player.transform.position = SpawnPoints[Index];
    }

    public void NewSafeColor()
    {
        SafeColor = ColorList[
            new List<string>(ColorList.Keys)[UnityEngine.Random.Range(0, ColorList.Count)]
        ];

        CreateTimer(5f, CanvasManager.HideUnsafeColors);
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
