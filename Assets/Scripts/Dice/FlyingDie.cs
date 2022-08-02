using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDie : DieController
{
    public GameObject Wings;

    public float FlyingHeight = 2.0f;


    protected override void Start()
    {
        base.Start();
        rb.gravityScale = 0;
        Fly(); 
    }

    void Fly()
    {
       
        transform.LeanMoveY(transform.position.y + FlyingHeight, 2.5f).setEaseInOutSine().setOnComplete(() =>
        { transform.LeanMoveY(transform.position.y - FlyingHeight, 2.5f).setEaseInOutSine().setOnComplete(() => Fly());
        });
    }

    public override void OnPickup(PlayerController user)
    {
        if (Wings.activeSelf)
        {
            rb.gravityScale = 1;
            Wings.SetActive(false);
            gameObject.LeanCancel();
        }
        base.OnPickup(user);
    }

    public override void DestroyAndRespawn(bool noParticles = false)
    {
        Wings.SetActive(true);
        rb.gravityScale = 0;
        transform.eulerAngles = Vector3.zero;
        base.DestroyAndRespawn(noParticles);
    }
}
