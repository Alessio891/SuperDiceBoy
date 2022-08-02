using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public TutorialBirb birb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController p = collision.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            birb.StartAnimation();
            Destroy(gameObject);
        }
    }
}
