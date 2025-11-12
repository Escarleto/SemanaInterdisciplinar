using System.Collections;
using System.Threading;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController Body;

    private Vector3 SpawnPoint;
    private Vector2 H_Input;
    private Vector2 MoveDir;
    private Vector2 DashDir = Vector2.right;
    private float V_Move;
    private float Speed = 4f;
    private bool Jumping = false;
    private bool CanDash = true;
    public bool CanRespawn = false;
    private bool Stunned = false;

    void Start()
    {
        Body = GetComponent<CharacterController>();
        SpawnPoint = transform.position;
        CanRespawn = false;
    }

    void Update()
    {

        if (CanDash)
        {
            MoveDir = (H_Input * Speed);
        }
        else
        {
            MoveDir = (DashDir * Speed);
        }

        Body.Move(new Vector3(MoveDir.x, V_Move, MoveDir.y) * Time.deltaTime);

        if (Body.isGrounded)
        {
            V_Move = -1f;
            if (Jumping)
            {
                V_Move = 4f;
                Jumping = false;
            }
        }
        else
        {
            V_Move += Physics.gravity.y * Time.deltaTime;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            PlayerController otherPlayer = hit.gameObject.GetComponent<PlayerController>();
            if (!CanDash)
            {
                Vector3 knockback = (hit.gameObject.transform.position - transform.position).normalized;
                otherPlayer.StartCoroutine(otherPlayer.KnockbackManager(otherPlayer, knockback, 5f));
            }
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        if (Stunned)
        {
            H_Input = Vector2.zero;
        }
        else
        {
            H_Input = ctx.ReadValue<Vector2>();
        }
        H_Input = H_Input.normalized;
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.action.triggered && Body.isGrounded)
        {
            Jumping = true;
        }
    }

    public void Dash(InputAction.CallbackContext ctx)
    {
        if (ctx.action.triggered && CanDash)
        {
            if (H_Input != Vector2.zero)
                DashDir = H_Input;
            StartCoroutine(DashManager());
        }
    }

    public void Respawn()
    {
        if(transform.position.y < -30f)
        {
            Body.enabled = false;
            transform.position = SpawnPoint;
            V_Move = 0f;
            H_Input = Vector2.zero;
            MoveDir = Vector2.zero;
            DashDir = Vector2.right;
            Speed = 4f;
            CanDash = true;
            Jumping = false;
            Stunned = false;
            Body.enabled = true;
        }
    }

    public IEnumerator DashManager()
    {
        CanDash = false;
        Speed = 12f;
        yield return new WaitForSeconds(0.2f);
        Speed = 4f;

        if (Body.isGrounded)
        {
            yield return new WaitForSeconds(0.2f);
            CanDash = true;
        }
        else
        {
            yield return new WaitUntil(() => Body.isGrounded == true);
            CanDash = true;
        }
    }

    private IEnumerator KnockbackManager(PlayerController otherPlayer, Vector3 direction, float force)
    {
        otherPlayer.Stunned = true;

        float startTime = Time.time;
        float endTime = startTime + 0.2f;
        do
        {
            float t = (Time.time - startTime) / 0.2f;
            Vector3 velocity = direction * force * (1 - t);

            otherPlayer.Body.Move(velocity * Time.deltaTime);

            yield return null;
        } while (Time.time < endTime);
        yield return new WaitUntil(() => otherPlayer.Body.isGrounded);
        otherPlayer.Stunned = false;
    }
}
