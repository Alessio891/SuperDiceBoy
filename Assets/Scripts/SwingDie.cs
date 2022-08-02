using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingDie : DieController
{
    public Rigidbody2D LastSegmentOfRope;
    FixedJoint2D Joint;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Update()
    {
        
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public override void OnPickup(PlayerController user)
    {
        user.transform.position = transform.position - transform.right * 0.5f - transform.up * 0.5f;
        user.GetComponent<PlayerMovement>().ResetJump();
        Joint = gameObject.AddComponent<FixedJoint2D>();
        Joint.connectedBody = user.GetComponent<Rigidbody2D>();
        user.GetComponent<CharacterController2D>().BypassGrounded = true;
        Sprite.sortingOrder = 2;

    }

    public override void OnDrop(PlayerController user)
    {
        Detach(user);

    }
    public override void OnThrow(PlayerController user, float MaxThrowForce, float charge)
    {
        Detach(user);
    }
    public override void OnKick(PlayerController user, float KickForce)
    {
        Detach(user);
    }

    public void Detach(PlayerController user)
    {
        Sprite.sortingOrder = 0;
        Destroy(Joint);
        Vector3 dir = user.GetComponent<CharacterController2D>().Forward;
        user.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
    }
}
