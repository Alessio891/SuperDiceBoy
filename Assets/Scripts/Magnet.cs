using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public SpriteRenderer DieSprite;
    public List<Sprite> DieSprites;
    public int Needed = 1;

    public float Range = 5.0f;
    public float AttractForce = 20.0f;
    bool grabbed = false;
    private void Start()
    {
        DieSprite.sprite = DieSprites[Needed - 1];
    }

    private void Update()
    {
        Vector3 playerPos = PlayerController.Player1.transform.position;
        float dist = Vector3.Distance(transform.position, playerPos);
        PlayerMovement movement = PlayerController.Player1.GetComponent<PlayerMovement>();
        if (dist <= Range)
        {
            Rigidbody2D rb = PlayerController.Player1.GetComponent<Rigidbody2D>();
            Vector2 dir = (playerPos - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, Range);
            if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                // Visibility
                if (PlayerController.Player1.Carrying && PlayerController.Player1.nearDice != null)
                {
                    int number = PlayerController.Player1.nearDice.GetComponent<DieController>().currentSprite + 1;
                    if (number == Needed)
                    {
                        if (!movement.Grabbed)
                        {
                            float perc = dist / Range;
                            float force = AttractForce * perc;                            
                            rb.AddForce(-dir * force * Time.deltaTime);
                            
                        }
                    } 
                } else
                {
                   
                }
            }
        } 
        if (movement.Grabbed)
        {
            Rigidbody2D rb = PlayerController.Player1.GetComponent<Rigidbody2D>();

            if (PlayerController.Player1.Carrying && PlayerController.Player1.nearDice != null)
            {
                int number = PlayerController.Player1.nearDice.GetComponent<DieController>().currentSprite + 1;
                if (number == Needed)
                {
                    movement.transform.position = transform.position+transform.right*0.6f;
                    rb.velocity = Vector2.zero;

                }
            } else
            {
                movement.Grabbed = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (PlayerController.Player1.Carrying && PlayerController.Player1.nearDice != null)
            {
                int number = PlayerController.Player1.nearDice.GetComponent<DieController>().currentSprite + 1;
                if (number == Needed)
                {
                    Rigidbody2D rb = PlayerController.Player1.GetComponent<Rigidbody2D>();
                    PlayerMovement movement = PlayerController.Player1.GetComponent<PlayerMovement>();
                    movement.Grabbed = true;
                }
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
