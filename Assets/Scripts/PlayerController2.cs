using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float Torque = 10.0f;
    public float JumpForce = 10.0f;
    bool jump = false;
    public bool landed = false;
    public List<Sprite> Sprites;
    SpriteRenderer sprite;

    float _fpsTimer = 0;
    public float SpriteSwapDelay = 0.5f;
    int currentSprite = 0;

    bool rolling = false;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (rolling)
            return;
        if (Input.GetKeyDown(KeyCode.Space) && !jump && landed)
        {
            landed = false;
            jump = true;
        }

      /*  if ( Mathf.Abs(GetComponent<Rigidbody2D>().angularVelocity) > 0.1f)
        {
            _fpsTimer += Time.deltaTime;
            if (_fpsTimer > SpriteSwapDelay)
            {
                currentSprite++;
                if (currentSprite >= Sprites.Count)
                    currentSprite = 0;
                sprite.sprite = Sprites[currentSprite];
                _fpsTimer = 0;
            }
        } else
        {
            _fpsTimer = 0;
        }*/
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (rolling)
            return;
        float turn = Input.GetAxis("Horizontal");
        //GetComponent<Rigidbody2D>().velocity = new Vector2(turn, 0).normalized * 5.0f;//AddTorque(-Torque * turn);
        if (jump)
        {
            GetComponent<Rigidbody2D>().AddForce(JumpForce * Vector3.up, ForceMode2D.Impulse);
            jump = false;
            landed = false;
        }
    }
    
    public void Roll(int rollTo) {
        if (!rolling)
            StartCoroutine(rollRoutine(rollTo));
    }

    IEnumerator rollRoutine(int rollTo)
    {
        rolling = true;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.angularVelocity = 0;
        rb.velocity = Vector2.zero;
        gameObject.LeanMove(transform.position + Vector3.up * 1.5f, 1.5f).setEaseOutSine();
        yield return new WaitForSeconds(1.5f);
        for(int i = 0; i < 10; i++)
        {
            sprite.sprite = Sprites[Random.Range(0, Sprites.Count)];
            yield return new WaitForSeconds(SpriteSwapDelay);
        }
        sprite.sprite = Sprites[rollTo - 1];
        yield return new WaitForSeconds(0.5f);
        landed = false;
        rb.gravityScale = 1;
        while (!landed)
            yield return null;
        rolling = false;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor") && !landed)
        {
            iTween.PunchScale(gameObject, new Vector3(1.2f, 0.7f, 1.0f), 0.3f);
            landed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
            landed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
            landed = true;
    }
}
