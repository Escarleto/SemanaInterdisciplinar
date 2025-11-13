using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public GameObject[] Hearts;
    public Sprite[] PlayerIcons;
    private RectTransform rctransform;
    private Vector2[] Positions = {
        new Vector2(-421f, 195f),
        new Vector2(-293f, 206.6f),
        new Vector2(-421f, 156.4f),
        new Vector2(-293f, 156.4f)};
    private int currentHP = 3;
    public int PlayerID;

    void Start()
    {
        rctransform = GetComponent<RectTransform>();
        GetComponent<Image>().sprite = PlayerIcons[PlayerID];
        //rctransform.localPosition = Positions[PlayerID];
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
