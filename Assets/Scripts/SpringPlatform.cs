using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringPlatform : MonoBehaviour
{
    public AudioClip SpringClip;
    AudioSource source;
    public SpriteRenderer DieSprite;
    public int NeededNumber = 1;
    public List<Sprite> Sprites;
    public float SpringForce = 10;
    Animator anim;
    bool usable = true;
    public bool AlwaysActive = false;    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!AlwaysActive)
            DieSprite.sprite = Sprites[NeededNumber - 1];
        else
            DieSprite.enabled = false;
        source.volume = source.volume * ((float)PlayerPrefs.GetInt("Volume", 10) / 10.0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!usable)
            return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")
            || collision.gameObject.layer == LayerMask.NameToLayer("Player 2"))
        {
            if (!AlwaysActive)
            {
                PlayerController p = collision.gameObject.GetComponent<PlayerController>();
                if (p.Carrying && p.nearDice != null)
                {
                    DieController d = p.nearDice.GetComponent<DieController>();
                    if (d.currentSprite == NeededNumber - 1)
                    {
                        Spring(collision.gameObject);
                    }
                }
            } else
            {
                Spring(collision.gameObject);
            }


        }
    }

    void Spring(GameObject collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        anim.SetBool("Spring", true);
        source.PlayOneShot(SpringClip);
        rb.velocity = Vector2.zero;
        rb.AddForce(transform.up * SpringForce, ForceMode2D.Impulse);
        StartCoroutine(ResetSpring());
    }

    IEnumerator ResetSpring()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Spring", false);
        usable = true;
    }
}
