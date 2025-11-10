using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject Tile;
    public CanvasManager CanvasManager;
    public Color SafeColor;
    public int Level;

    public Dictionary<string, Color> ColorList = new Dictionary<string, Color>()
    {
        {"Red", Color.red},
        {"Green", Color.green},
        {"Blue", Color.cyan},
        {"Yellow", Color.yellow},
        {"Orange", new Color(1f, 0.6f, 0f) },
        {"Purple", new Color(0.5f, 0f, 0.5f) }
    };

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Level = 1;
    }

    public void NewSafeColor()
    {
        SafeColor = ColorList[
        new List<string>(ColorList.Keys)[UnityEngine.Random.Range(0, ColorList.Count)]];
        Level += 1;
    }

    public void CreateTimer(float duration, Action onComplete)
    {
        StartCoroutine(TimerCoroutine(duration, onComplete));
    }

    private IEnumerator TimerCoroutine(float duration, Action onComplete)
    {
        yield return new WaitForSeconds(duration);
        onComplete?.Invoke();
    }
}
