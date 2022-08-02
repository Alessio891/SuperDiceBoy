using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    BoxCollider2D coll;
    SpriteRenderer sprite;
    Animator animator;
    public Text NumberText;
    public DoorSwitch LinkedSwitch;
    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        NumberText.text = (LinkedSwitch.Number > 0) ? LinkedSwitch.Number.ToString() : "*";
    }
    public void Open() {
        coll.enabled = false;
        animator.SetBool("Open", true);   
    }
    public void Close() {
        animator.SetBool("Open", false);
        coll.enabled = true;
    }
}
