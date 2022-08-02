using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject nearDice = null;
    public bool Carrying = false;

    public GameObject Canvas;
    public Image ChargeBar;

    public float MaxThrowForce = 9.0f;
    public float MinThrowForce = 2.0f;
    public float ChargeSpeed = 0.2f;
    float currentCharge = 0;
    bool charging = false;
    public Transform GroundCheck;
    public float EnemyBounceForce = 5.0f;
    public LayerMask EnemyLayer;
    bool JumpedOnEnemy = false;
    public float KickForce = 30.0f;
    public ParticleSystem KickSfx;
    public int PlayerIndex = 0;
    float oldTrigger = 0;
    CharacterController2D charController;
    public System.Action<float> OnThrowDie = (f) => { };

    public int CurrentNumber
    {
        get
        {
            if (Carrying && nearDice != null)
            {
                return nearDice.GetComponent<DieController>().currentSprite + 1;
            }
            return -1;
        }
    }

    public static PlayerController Player1, Player2;
    private void Awake()
    {
        charController = GetComponent<CharacterController2D>();
        if (PlayerIndex == 0)
            Player1 = this;
        else
        {
            if (PlayerPrefs.GetInt("Coop", 0) == 1)
            {
                Player2 = this;
            } else
            {
                Player2 = null;
                gameObject.SetActive(false);
            }
        }
    }
    private void Start()
    {
        if (Player1 == this && Player2 != null)
        {
            GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard & Mouse");
        }
        ChargeBar.fillAmount = 0;
        Canvas.SetActive(false);        
    }

    public void ResetToLastPosition()
    {
        transform.position = charController.LastGroundPosition;
        charController.m_Rigidbody2D.velocity = Vector2.zero;

        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up + Vector3.right, Vector3.down, 5.0f, LayerMask.GetMask("Floor"));
        if (hit.collider == null)
        {
            charController.LastGroundPosition = transform.position - transform.right;
            ResetToLastPosition();
            return;
        }
        hit = Physics2D.Raycast(transform.position + Vector3.up - Vector3.right, Vector3.down, 5.0f, LayerMask.GetMask("Floor"));
        if (hit.collider == null)
        {
            charController.LastGroundPosition = transform.position + transform.right;
            ResetToLastPosition();
            return;
        }


    }

    public void OnLeaveGround()
    {
       
    }

    public AudioClip PickupSfx, ThrowSfx, KickSound;
    private void Update()
    {
        if (GameMenu.instance.Open)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            return;
        }
        if (charController.Grounded)
        {
            
        }
        if (!Carrying)
        {
            RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, 1.5f, transform.right, 0);
            nearDice = null;
            foreach (RaycastHit2D h in hit)
            {
                if (h.collider.gameObject.layer == LayerMask.NameToLayer("Dice"))
                {
                    nearDice = h.collider.gameObject;
                }
            }
        } else
        {
            if (nearDice == null)
            {
                Carrying = false;
            }
        }
        
        if (charging)
        {
            if (!Carrying || nearDice == null)
            {
                charging = false;
                Canvas.SetActive(false);
                return;
            }
            if (!Canvas.activeSelf)
                Canvas.SetActive(true);

            currentCharge += ChargeSpeed * Time.deltaTime;
            if (currentCharge > 1)
                currentCharge = 1;
            ChargeBar.fillAmount = currentCharge;
        }        
    }

    public void DropButton(InputAction.CallbackContext ctx) {
        if (ctx.performed) return;
        if (!charging && Carrying && nearDice != null)
        {

            nearDice.GetComponent<DieController>().OnDrop(this);
            Carrying = false;
            nearDice = null;
        }
    }
    public void KickButton(InputAction.CallbackContext ctx) {
        if (ctx.performed) return;
        if (!charging && Carrying && nearDice != null)
        {
            ParticleSystem p = GameObject.Instantiate<ParticleSystem>(KickSfx);
            p.transform.position = nearDice.transform.position;
            nearDice.GetComponent<DieController>().OnKick(this, KickForce);
            Carrying = false;
            nearDice = null;
            GetComponent<AudioSource>().PlayOneShot(KickSound);


        }
    }

    public void ThrowButton(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) return;
        bool pressed = ctx.ReadValueAsButton();
        if (pressed && nearDice != null)
        {
            if (!Carrying)
            {
                nearDice.GetComponent<DieController>().OnPickup(this);
                Carrying = true;
                GetComponent<AudioSource>().PlayOneShot(PickupSfx);
            }
            else
            {
                charging = true;
            }
        }

        if (!pressed && Carrying && charging & nearDice != null)
        {
            OnThrowDie(currentCharge);
            nearDice.GetComponent<DieController>().OnThrow(this, MaxThrowForce, currentCharge);
            Carrying = false;
            nearDice = null;
            GetComponent<AudioSource>().PlayOneShot(ThrowSfx);
            currentCharge = 0;
            Canvas.SetActive(false);
            charging = false;
        }
    }

    public void RemoveCarrying()
    {
        if (nearDice != null && Carrying)
        {
            nearDice.transform.SetParent(LevelManager.instance.CurrentLevelComponent.transform);            
            nearDice = null;
            Carrying = false;
        }
    }

    private void OnGUI()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        GUILayout.Label("Scheme: " + input.currentControlScheme);
        GUILayout.Label("Never Autoswtch: " + input.neverAutoSwitchControlSchemes);
        
    }

    private void FixedUpdate()
    {    }

    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.1f);
        JumpedOnEnemy = false;
    }
}
