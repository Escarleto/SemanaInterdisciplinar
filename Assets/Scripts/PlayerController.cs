using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 H_move;
    private Transform Mesh;
    private float Speed = 10f;

    void Start()
    {
        Mesh = transform.Find("Mesh");
    }

    void Update()
    {
        H_move.x = Input.GetAxisRaw("Horizontal");
        H_move.y = Input.GetAxisRaw("Vertical");
        H_move.Normalize();

        transform.Translate(new Vector3(H_move.x, 0f, H_move.y) * Speed * Time.deltaTime);
    }
}
