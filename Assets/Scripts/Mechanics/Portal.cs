using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal LinkedPortal;
    public int Needed = 1;
    public bool Active = true;
    public SpriteRenderer DieSprite, DisabledSprite;    

    private void Start()
    {
        if (Needed == -1)
        {
            DisabledSprite.enabled = true;
        }
        else
        {
            DisabledSprite.enabled = false;
            DieSprite.sprite = LevelManager.instance.DieSides[Needed - 1];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Active)
            return;
        PlayerController p = collision.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            if (LinkedPortal != null && p.CurrentNumber == Needed)
            {
                StartCoroutine(TeleportRoutine(p));
            } else if (Needed == -1)
            {
                if (!p.Carrying)
                {
                    StartCoroutine(TeleportRoutine(p));
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!Active)
        {
            if (collision.gameObject.GetComponent<PlayerController>() != null)
                Active = true;
        }
    }

    IEnumerator TeleportRoutine(PlayerController user)
    {
        LinkedPortal.Active = false;
        yield return null;
        user.transform.position = LinkedPortal.transform.position;
        
    }
}
