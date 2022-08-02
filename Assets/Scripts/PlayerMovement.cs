using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GameObject Sprite;
    public float runSpeed = 40f;

    protected CharacterController2D controller;
    protected float horizontalMove = 0f;
    protected bool jump = false;
    protected bool wasMoving = false;

    protected bool Jumping = false;
    protected bool falling = false;
    protected bool climbing = false;

    public float Acceleration = 10.0f;

    public UnityEvent OnStartMoving, OnStopMoving, OnLeaveGround, OnStartFall;

    private PlayerController PlayerController;

    public AudioClip JumpSound;
    AudioSource source;
    Animator animator;

    public Transform GroundCheck;

    bool holdingJump = false;    
    public float MaxJumpForce = 600.0f;
    public float MinJumpForce = 200.0f;
    public float JumpChargeTime = 0.35f;
    float jumpChargeTimer = 0;
    float _jumpTimer = 0;
    [SerializeField] float JumpBufferTimer = 0.2f;

    Coroutine JumpBufferRoutine;

    public bool Grabbed = false;

    protected void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        PlayerController = GetComponent<PlayerController>();
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    protected virtual void Start()
    {
        if (OnStartMoving == null)
            OnStartMoving = new UnityEvent();
        if (OnStopMoving == null)
            OnStopMoving = new UnityEvent();
        if (OnLeaveGround == null)
            OnLeaveGround = new UnityEvent();
        if (OnStartFall == null)
            OnStartFall = new UnityEvent();
        
    }

    public void SetMovement(InputAction.CallbackContext value)
    {
        horizontalMove = value.ReadValue<float>() * runSpeed;
        if (horizontalMove != 0 && !wasMoving)
        {
            wasMoving = true;
            OnStartMoving.Invoke();
        }
        else if (horizontalMove == 0 && wasMoving)
        {
            wasMoving = false;
            OnStopMoving.Invoke();
        }
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (!gameObject.activeInHierarchy)
            return;

        if (!ctx.ReadValueAsButton())
        {
            holdingJump = false;
        }
        else
        {
            if (jumpChargeTimer < JumpChargeTime)
            {
                holdingJump = true;
            }
        }

        if (ctx.started)
        {
            _jumpTimer = JumpBufferTimer;            
        }
    }

    protected virtual void Update()
    {
       
        if (!Jumping && !controller.Grounded)
        {
            if (!falling && !climbing)
            {
                OnStartFall.Invoke();
                falling = true;
            }
        }

        _jumpTimer -= Time.deltaTime;

    }

    void FixedUpdate()
    {
        if (GameMenu.instance.Open)
            return;

        jump = _jumpTimer > 0;

        controller.Move(horizontalMove*Time.fixedDeltaTime, false, jump);
        if (jump)
        {            
            if (!Jumping)
            {
                if (!falling)
                    source.PlayOneShot(JumpSound);
                Jumping = true;
                animator.SetTrigger("Jump");
                OnLeaveGround.Invoke();
                _jumpTimer = 0;
            }
        }
        //jump = false;
        /*if (controller.Grounded)
            jump = false;
        else
        {
            StopAllCoroutines();
            StartCoroutine(JumpBuffer());
        } */   
        
    }

    IEnumerator JumpBuffer()
    {
        yield return null;
        yield return null;
        yield return null;
        jump = false;
    }

    public void OnLand()
    {
        if (Jumping)
            Jumping = false;
        if (falling)
            falling = false;
        if (climbing)
            climbing = false;
        Debug.Log("Landed");
        animator.SetTrigger("Land");

    }

    public void WalkAnimation() {
        if (controller.Grounded)
            animator.SetBool("Moving", true);
    }
    public void StopWalkAnimation() {
        animator.SetBool("Moving", false);
    }

}
