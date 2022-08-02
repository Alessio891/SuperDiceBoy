using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirbController : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sprite;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public bool Scared = false;
    public float FlightSpeed = 3.0f;
    float _fleeingTimer = 0;
    float _changeDirTimer = 0;
    Transform currentTarget;
    bool goBack = false;

    private void Start()
    {
        PlayerController.Player1.OnThrowDie += (f) =>
        {
            if (currentTarget != null || goBack)
                return;
            float dist = Vector3.Distance(transform.position, PlayerController.Player1.nearDice.transform.position);
            if (dist <= 10.0f)
            {
                currentTarget = PlayerController.Player1.nearDice.transform;
                animator.SetBool("Flap", true);
            }
        };
    }

    private void Update()
    {
        if (!goBack)
        {
            if (currentTarget != null)
            {
                Vector3 dir = (currentTarget.position - transform.position).normalized;
                transform.position += dir * FlightSpeed * Time.deltaTime;
            }
        } else
        {
            if (currentTarget != null)
            {
                Vector3 dir = (currentTarget.position - transform.position).normalized;
                transform.position += dir * FlightSpeed * Time.deltaTime;
                if (Vector3.Distance(transform.position, currentTarget.position) < 0.1f)
                {
                    transform.position = currentTarget.position;
                    currentTarget = null;
                    goBack = false;
                    animator.SetBool("Flap", false);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DieController d = collision.gameObject.GetComponent<DieController>();
        if (d != null)
        {
            goBack = true;
            currentTarget = LevelManager.instance.CurrentLevelComponent.RandomBirdPosition;
        }
    }


}
