using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController Body;
    private InputSystem_Actions input;

    private Vector3 SpawnPoint;
    private Vector2 H_Input;
    private Vector2 MoveDir;
    private Vector2 DashDir = Vector2.right;
    private float V_Move;
    private float Speed = 4f;
    private bool Jumping = false;
    private bool CanDash = true;

    void OnEnable()
    {
        input = new InputSystem_Actions();
        input.Enable();

        input.Player.Move.performed += ctx => Move(ctx);
        input.Player.Move.canceled += ctx => Move(ctx);
        input.Player.Jump.performed += ctx => Jump(ctx);
        input.Player.Dash.performed += ctx => Dash(ctx);
    }

    void OnDisable()
    {
        input.Player.Move.performed -= ctx => Move(ctx);
        input.Player.Move.canceled -= ctx => Move(ctx);
        input.Player.Jump.performed -= ctx => Jump(ctx);
        input.Player.Dash.performed -= ctx => Dash(ctx);
        input.Disable();
    }

    void Start()
    {
        Body = GetComponent<CharacterController>();
        SpawnPoint = transform.position;
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

        if (transform.position.y < -10f)
        {
            transform.position = SpawnPoint;
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        H_Input = ctx.ReadValue<Vector2>();
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

    public IEnumerator DashManager()
    {
        CanDash = false;
        Speed = 12f;
        yield return new WaitForSeconds(0.2f);
        Speed = 3.5f;

        if(Body.isGrounded)
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
}
