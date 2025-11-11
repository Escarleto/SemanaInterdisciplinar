using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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
