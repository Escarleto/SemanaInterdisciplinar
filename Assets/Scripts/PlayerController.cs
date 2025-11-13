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
    private TrailRenderer Trail;
    public GameObject[] Skins; 
    public AudioClip DashSFX;
    public AudioClip JumpSFX;
    public AudioClip FallSFX;
    public AudioClip StunnedSFX;
    public AudioClip[] SkinSFX;

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
        Trail = GetComponent<TrailRenderer>();
        SpawnPoint = transform.position;

        for (int i = 0; i < Skins.Length; i++)
        {
            if (i == PlayerID)
            {
                Material[] mats = Trail.materials;
                mats[0] = Skins[i].GetComponentInChildren<Renderer>().material;
                Trail.materials = mats;

                Skins[i].SetActive(true);
            }
        }
    }

    void Update()
    {
        if (!Body.enabled)
        {
            return;
        }

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
            Trail.emitting = true;
            V_Move = -1f;
            if (Jumping)
            {
                V_Move = 4f;
                Jumping = false;
            }
        }
        else
        {
            Trail.emitting = false;
            V_Move += Physics.gravity.y * Time.deltaTime;
        }
        
        if (transform.position.y < -5f && Audio.clip != FallSFX)
        {
            Sound_Handler(FallSFX);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            PlayerController otherPlayer = hit.gameObject.GetComponent<PlayerController>();
            if (!CanDash)
            {
                Sound_Handler(StunnedSFX);
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
            Sound_Handler(JumpSFX);
        }
    }

    public void Dash(InputAction.CallbackContext ctx)
    {
        if (ctx.action.triggered && CanDash)
        {
            Sound_Handler(DashSFX);
            if (H_Input != Vector2.zero)
                DashDir = H_Input;
            StartCoroutine(DashManager());
        }
    }

    public void beReady(InputAction.CallbackContext ctx)
    {
        if (ctx.action.triggered && !Ready)
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
            Body.enabled = false;
            GameManager.Instance.PlayersAlive -= 1;
        }
    }

    private void Sound_Handler(AudioClip clip)
    {
        Audio.clip = clip;
        Audio.pitch = Random.Range(0.8f, 1.2f);
        Audio.Play();
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
            if (!otherPlayer.Body.enabled) yield break;

            float t = (Time.time - startTime) / 0.2f;
            Vector3 velocity = direction * force * (1 - t);

            otherPlayer.Body.Move(velocity * Time.deltaTime);

            yield return null;
        } while (Time.time < endTime);
        yield return new WaitUntil(() => otherPlayer.Body.isGrounded);
        otherPlayer.Stunned = false;
    }
}
