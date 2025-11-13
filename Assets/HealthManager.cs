using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float x = 0f;
    public float y = 0f;

    public GameObject[] Hearts;
    public Sprite[] PlayerIcons;
    private RectTransform rctransform;
    private int currentHP = 3;
    public int PlayerID;

    void Start()
    {
        Vector2[] Positions = new Vector2[] {
            new Vector2(112f, 125f),
            new Vector2(380f, 125f),
            new Vector2(651f, 125f),
            new Vector2(900f, 125f)
        };
        rctransform = GetComponent<RectTransform>();
        GetComponent<Image>().sprite = PlayerIcons[PlayerID];
        
       rctransform.position = Positions[PlayerID];
    }

    public void UpdateHP(PlayerController player)
    {
        if (currentHP != player.HP && currentHP > 0)
        {
            Animator HeartBreak = Hearts[currentHP - 1].GetComponent<Animator>();
            HeartBreak.enabled = true;

            currentHP = player.HP;
        }
        else
        {
            return;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
