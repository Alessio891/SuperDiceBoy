using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBirb : MonoBehaviour
{
    public GameObject DialogueCanvas;
    public Text TextComponent;
    public string TutorialText;

    public Transform TargetPosition;
    Animator animator;

    public float Speed = 5.0f;
    float _timer = 0;
    int state = 0; // 0 = nothing 1 = fly to pos 2 = show tutorial 3 = fly away
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        TextComponent.text = TutorialText;
        DialogueCanvas.SetActive(false);
    }
    [ContextMenu("Start fly")]
    public void StartAnimation()
    {
        animator.SetBool("Flap", true);
        gameObject.LeanMove(TargetPosition.position, 1.0f).setSpeed(Speed).setEaseOutSine().setOnComplete(() => {
            DialogueCanvas.SetActive(true);
            animator.SetBool("Flap", false);
        });
    }

    private void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state != 3)
            return;
        PlayerController p = collision.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            
            DialogueCanvas.SetActive(false);
            animator.SetBool("Flap", true);
            state = 3;
            
        }
    }

    void FlyToTarget()
    {
        animator.SetBool("Flap", true);

        Vector3 dir = (TargetPosition.position - transform.position).normalized;
        transform.position += dir * Speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, TargetPosition.position) < 0.1f)
        {
            animator.SetBool("Flap", false);
            transform.position = TargetPosition.position;
            state = 2;
        }
    }
    void FlyAway()
    {
        Vector3 dir = Vector3.up + Vector3.left;
        transform.position += dir * Speed * Time.deltaTime;
        Vector3 worldPos = Camera.main.WorldToScreenPoint(transform.position);
        if (worldPos.x < 0 || worldPos.y < 0)
            Destroy(gameObject);
    }
}
