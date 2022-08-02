using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveDie : DieController
{
    bool thrown = false;
    bool wasStopped = false;
    public override int Thrown { get => base.Thrown; 
        set 
        {
            base.Thrown = value;
            if (value > 0 && !thrown)
            {
                thrown = true;
                //gameObject.layer = LayerMask.NameToLayer("Default");
                //GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
    float _explosionTimer = 0;
    protected override void Update()
    {
        base.Update();

        if (thrown)
        {
            if (rb.velocity.magnitude <= 0.1f)
            {
                if (!wasStopped)
                {
                    wasStopped = true;
                    gameObject.LeanValue((f) => Sprite.color = new Color(f, 0, 0), 0.0f, 1.0f, 0.4f).setEaseInOutSine().setLoopPingPong();
                    //gameObject.LeanScale(new Vector3(1.5f, 1.5f, 1), 0.3f).setEaseInOutSine().setLoopPingPong();
                }
                _explosionTimer += Time.deltaTime;
                if (_explosionTimer > 2.0f)
                {
                    gameObject.layer = LayerMask.NameToLayer("Dice");
                    Sprite.color = Color.white;
                    DestroyAndRespawn();
                }
            }
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
            
    }
}
