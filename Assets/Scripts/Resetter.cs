using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resetter : MonoBehaviour
{
    public Transform StartPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DieController d = collision.gameObject.GetComponent<DieController>();
        PlayerController p = collision.gameObject.GetComponent<PlayerController>();
        
        if (d != null)
        {
            d.DestroyAndRespawn();
        }
        else if (p != null)
        {
            if (p.Carrying)
            {
                GameObject dice = p.nearDice;
                p.RemoveCarrying();
                if (dice != null)
                    dice.GetComponent<DieController>().DestroyAndRespawn();
            }
            p.ResetToLastPosition();
        }
    }
}
