using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSpring : MonoBehaviour
{
    public float Force = 25.0f;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DieController d = collision.gameObject.GetComponent<DieController>();
        if (d != null)
        {
            animator.SetTrigger("Spring");
            d.GetComponent<Rigidbody2D>().AddForce(transform.up * Force, ForceMode2D.Impulse);
        }
    }
}
