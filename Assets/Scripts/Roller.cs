using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller : MonoBehaviour
{

    public int Number = 1;
    public bool Used = false;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Used)
            return;
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.transform.position = transform.position;
            Used = true;
            other.gameObject.GetComponent<PlayerController2>().Roll(Number);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Used)
            Used = false;
    }
}
