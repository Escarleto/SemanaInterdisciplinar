using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject Player;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }
}
