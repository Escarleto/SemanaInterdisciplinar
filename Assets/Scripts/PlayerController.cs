using System.Collections;
using System.Threading;
using TreeEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController Body;
    private AudioSource Audio;
    public GameObject[] Skins; 
    public AudioClip DashSFX;
    public AudioClip JumpSFX;
    public AudioClip FallSFX;
    public AudioClip StunnedSFX;

    private Vector3 SpawnPoint;
    private Vector2 H_Input;
    private Vector2 MoveDir;
    private Vector2 DashDir = Vector2.right;
    private float V_Move;
    private float Speed = 4f;
    public int HP = 3;
    public int PlayerID;
    public bool Ready = false;
    private bool Jumping = false;
    private bool CanDash = true;
    private bool Stunned = false;

    void Start()
    {
        Body = GetComponent<CharacterController>();
        Audio = GetComponent<AudioSource>();
        SpawnPoint = transform.position;

        for (int i = 0; i < Skins.Length; i++)
        {
            if (i == PlayerID)
            {
                TrailRenderer trail = GetComponent<TrailRenderer>();
                Material[] mats = trail.materials;
                mats[0] = Skins[i].GetComponentInChildren<Renderer>().material;
                trail.materials = mats;

                Skins[i].SetActive(true);
            }
        }
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

        if (transform.position.y < -2f && Audio.clip != FallSFX)
        {
            Audio.clip = FallSFX;
            Audio.Play();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            PlayerController otherPlayer = hit.gameObject.GetComponent<PlayerController>();
            if (!CanDash)
            {
                Audio.clip = StunnedSFX;
                Audio.Play();

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
            Audio.clip = JumpSFX;
            Audio.Play();
        }
    }

    public void Dash(InputAction.CallbackContext ctx)
    {
        if (ctx.action.triggered && CanDash)
        {
            Audio.clip = DashSFX;
            Audio.Play();
            if (H_Input != Vector2.zero)
                DashDir = H_Input;
            StartCoroutine(DashManager());
        }
    }

    public void beReady(InputAction.CallbackContext ctx)
    {
        if (ctx.action.triggered)
        {
            Ready = true;
            GameManager.Instance.StartGame();
        }
    }

    public void HP_Handler()
    {
        if (HP > 0)
        {
            if (transform.position.y < -5f)
            {
                HP -= 1;
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
        else
        {
            gameObject.SetActive(false);
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
