using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameObject[] Hearts;
    private int currentHP = 3;

    void Start()
    {

    }

    public void UpdateHP(PlayerController player)
    {
        if (currentHP != player.HP && currentHP > 0)
        {
            Animator HeartBreak = Hearts[currentHP - 1].GetComponent<Animator>();
            HeartBreak.Play("Image");

            currentHP = player.HP;
        }
        else
        {
            return;
        }
    }
}
