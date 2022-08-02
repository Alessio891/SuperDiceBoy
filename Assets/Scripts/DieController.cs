using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieController : MonoBehaviour
{
    public List<Sprite> Sprites;
    protected SpriteRenderer Sprite;
    protected Rigidbody2D rb;
    public int currentSprite = 0;

    public float AnimDelay = 0.1f;
    public AudioClip BounceSound, DisappearSound;
    public List<Color> Colors;
    protected AudioSource source;

    protected float _timer = 0;

    public GameObject Canvas;
    public Text NumberText;
    protected int _thrown = 0;
    public int MaxThrown = 3;

    public bool DoNotReset = false;
    public virtual int Thrown
    {
        get
        {
            return _thrown;
        }
        set
        {
            _thrown = value;
            if (value < Colors.Count)
                Sprite.color = Colors[value];
            /*switch(value)
            {                
                case 1:
                    transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                    break;
                case 2:
                    transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    break;
                case 3:
                    transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                    gameObject.LeanScale(new Vector3(1.4f, 1.4f, 1.4f), 0.8f).setEaseOutSine().setLoopPingPong();
                    break;
            }*/
        }
    }

    public ParticleSystem DisappearParticles;

    public Vector3 originalPos;
    public bool RegisterPos = true;
    public Vector3 RespawnOffset = new Vector3(0, 1.5f);
    LayerMask CollisionMask;
    
    protected virtual void Awake()
    {
        Sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        if (NumberText != null)
            NumberText.enabled = false;
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (RegisterPos)
            originalPos = transform.position;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        float delay = AnimDelay;// * (rb.angularVelocity / Physics2D.maxRotationSpeed);
        if (Mathf.Abs(rb.angularVelocity) > 0.5f)
        {
            if (NumberText != null)
            {
                if (!NumberText.enabled)
                {
                    NumberText.enabled = true;
                }
                NumberText.text = (currentSprite + 1).ToString();
            }
            _timer += Time.deltaTime;
            if (_timer >= delay)
            {
                currentSprite++;
                if (currentSprite >= Sprites.Count)
                    currentSprite = 0;
                Sprite.sprite = Sprites[currentSprite];
                _timer = 0;
            }
        } else
        {
            if (NumberText != null)
            {
                if (NumberText.enabled)
                    NumberText.enabled = false;
            }
        }
        if (Canvas != null)
            Canvas.transform.position = transform.position + Vector3.up*1.5f;
    }
    public virtual void OnPickup(PlayerController user) {
        transform.SetParent(user.transform);
        Sprite.sortingOrder = 3;
        rb.velocity = Vector2.zero;
        rb.simulated = false;
        bool facingRight = user.GetComponent<CharacterController2D>().FacingRight;
        transform.localPosition = new Vector3((facingRight) ? 0.5f : -0.5f,0.5f , 0);
        transform.eulerAngles = Vector3.zero;
        transform.localScale = Vector3.one;
        rb.angularVelocity = 0;
    }
    public virtual void OnThrow(PlayerController user, float MaxThrowForce, float charge) { StartCoroutine(throwroutine(user, MaxThrowForce, charge)); }
    public virtual void OnDrop(PlayerController user) {
        transform.SetParent(LevelManager.instance.CurrentLevelComponent.transform);
        rb.simulated = true;
        transform.localScale = Vector3.one;
    }
    public virtual void OnKick(PlayerController user, float KickForce) {
        Vector3 pointerPos = (user.PlayerIndex == 0) ? PointerOverride.Player1Pointer.transform.position : PointerOverride.Player2Pointer.transform.position;
        Vector2 dir = (pointerPos - Camera.main.WorldToScreenPoint(transform.position)).normalized;
        transform.SetParent(LevelManager.instance.CurrentLevelComponent.transform);
        transform.localScale = Vector3.one;
        rb.simulated = true;
        Thrown++;
        rb.AddForce(dir * KickForce, ForceMode2D.Impulse);
        user.GetComponent<Rigidbody2D>().AddForce(-dir * KickForce, ForceMode2D.Impulse);
        CameraController.instance.Shake();        
    }

    IEnumerator throwroutine(PlayerController user, float MaxThrowForce, float currentCharge)
    {
        Vector3 pointerPos = (user.PlayerIndex == 0) ? PointerOverride.Player1Pointer.transform.position : PointerOverride.Player2Pointer.transform.position;
        Vector2 dir = (pointerPos - Camera.main.WorldToScreenPoint(transform.position)).normalized;
        transform.SetParent(LevelManager.instance.CurrentLevelComponent.transform);
        transform.localScale = Vector3.one;
        rb.simulated = true;
        rb.velocity = Vector2.zero;
        yield return null;
        if (rb != null)
        {

            Vector2 force = dir * MaxThrowForce * currentCharge;

            // if (force.magnitude < MinThrowForce)
            //   force = force.normalized * MinThrowForce;
            rb.AddForce(force, ForceMode2D.Impulse);
            rb.AddTorque(0.5f, ForceMode2D.Impulse);
        }
        float shake = 3.5f;
        if (currentCharge >= 0.8f)
        {
            shake = 15.0f;
            //rb.AddForce(-dir.normalized * 5.0f, ForceMode2D.Impulse);
            //transform.position -= new Vector3(dir.normalized.x, dir.normalized.y, 0)*1.8f;
        }
        Thrown++;
        
        CameraController.instance.Shake(shake);
       
        
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionMask = LayerMask.GetMask("Floor",  "Platform");
        if (CollisionMask == (CollisionMask | (1<<collision.gameObject.layer)))
        {
            if (Thrown >= MaxThrown)
            {
                DiceManager.instance.DieExploded(this);
                DestroyAndRespawn();
            } else
                source.PlayOneShot(BounceSound);
        }
    }

    public virtual void DestroyAndRespawn(bool noParticles = false)
    {
        gameObject.LeanCancel();
        rb.simulated = true;
        DieController d = GameObject.Instantiate<DieController>(this);
        d.transform.position = originalPos + RespawnOffset;
        d.RegisterPos = false;
        d.originalPos = originalPos;
        d.Thrown = 0;
        d.transform.localScale = Vector3.one;
        d.transform.SetParent(LevelManager.instance.CurrentLevelComponent.transform);
        if (!noParticles)
        {
            ParticleSystem p = GameObject.Instantiate<ParticleSystem>(DisappearParticles);
            p.transform.position = transform.position;
        }
        GameObject o = new GameObject();
        AudioSource s = o.AddComponent<AudioSource>();
        s.volume = source.volume;
        s.PlayOneShot(DisappearSound);
        Destroy(o, 1.0f);
        Destroy(gameObject);
    }

    public virtual void ResetDie() { 
        
    }
}
