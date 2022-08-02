using Spektr;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zapper : MonoBehaviour
{
    LightningRenderer[] lr;
    bool active = true;
    public bool Move = false;
    public bool Horizontal = true;
    public float Speed = 3.0f;
    public float Range = 4.0f;
    Vector3 startPos;
    bool changeDir = false;
    private void Awake()
    {
        lr = GetComponentsInChildren<LightningRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (LightningRenderer r in lr)
            r.enabled = true;

        if (Move)
        {
            startPos = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Move)
        {
            transform.position += ((Horizontal) ? -Vector3.right : -Vector3.up) * Speed * Time.deltaTime * ((changeDir) ? -1 : 1);

            float dist = (Horizontal) ? Mathf.Abs(startPos.x - transform.position.x) : Mathf.Abs(startPos.y - transform.position.y);

            if (dist >= Range)
                changeDir = !changeDir;
            /*
            if (transform.position.x > startPos.x && changeDir)
            {
                if (transform.position.x - startPos.x >= Range)
                    changeDir = false;
            }
            else if (transform.position.x < startPos.x && !changeDir)
            {
                if (Mathf.Abs(startPos.x-transform.position.x) >= Range)
                    changeDir = true;
            }*/
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player")
                || collision.gameObject.layer == LayerMask.NameToLayer("Player 2"))
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                if (player.nearDice != null && player.Carrying)
                {
                    player.nearDice.GetComponent<DieController>().DestroyAndRespawn();
                    player.nearDice = null;
                    player.Carrying = false;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (active)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player")
                || collision.gameObject.layer == LayerMask.NameToLayer("Player 2"))
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                if (player.nearDice != null && player.Carrying)
                {
                    player.nearDice.GetComponent<DieController>().DestroyAndRespawn();
                    player.nearDice = null;
                    player.Carrying = false;
                }
            }
        }
    }
}
