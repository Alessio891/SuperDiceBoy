using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorSwitch : MonoBehaviour
{
    public int Number = 1;

    public Door LinkedDoor;
    AudioSource source;
    public AudioClip OpenClip;
    List<DieController> currentColliding = new List<DieController>();
    bool Activated = false;
    public bool SwitchOnContact = false;
    bool toggled = false;
    public UnityEvent OnActivate, OnDeactivate;
    public bool ParentDie = false;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (OnActivate == null)
            OnActivate = new UnityEvent();
        if (OnDeactivate == null)
            OnDeactivate = new UnityEvent();
        source.volume = source.volume * ((float)PlayerPrefs.GetInt("Volume", 10) / 10.0f);

    }

    bool animating = false;
    private void Update()
    {
        if (animating)
        {
            Animator anm = GetComponent<Animator>();
            if (anm.GetCurrentAnimatorStateInfo(0).IsName("off") || anm.GetCurrentAnimatorStateInfo(0).IsName("on"))
            {
                AnimationEnd();
            }
            return;
        }
        
        if (SwitchOnContact)
            return;

        int number = 0;
        for (int i = 0; i < currentColliding.Count; i++)
        {
            DieController d = currentColliding[i];
            if (d == null)
            {
                currentColliding.RemoveAt(i);
                i--;
                continue;
            }
            number += d.currentSprite + 1;
        }
            
        if (number == Number || (Number < 0 && currentColliding.Count > 0))
        {
            if (!Activated)
            {
                Activate();
            }
        }
        else
        {
            if (Activated)
            {
                Deactivate();
            }
        }
        
    }
    IEnumerator delayedReactivation()
    {
        yield return new WaitForSeconds(0.2f);
        Activated = !Activated;
    }
    void Activate()
    {
        animating = true;
        GetComponent<Animator>().SetBool("Activate", true);
        source.PlayOneShot(OpenClip);
        OnActivate.Invoke();
        Activated = true;
    }
    void Deactivate()
    {
        animating = true;
        GetComponent<Animator>().SetBool("Activate", false);
        OnDeactivate.Invoke();
        Activated = false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DieController die = collision.gameObject.GetComponent<DieController>();
        
        if (die != null)
        {
            if (!SwitchOnContact)
            {
                if (!currentColliding.Contains(die))
                {
                    currentColliding.Add(die);
                }
            } else
            {
                if (die.currentSprite == Number-1 || Number < 0)
                {
                    if (Activated)
                    {
                        Deactivate();
                    } else
                    {
                        Activate();
                    }
                }
            }
        }
    }

    public void AnimationEnd()
    {
        Debug.Log("End anim");
        animating = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (SwitchOnContact)
            return;
        DieController die = collision.gameObject.GetComponent<DieController>();
        if (die != null)
        {
            if (currentColliding.Contains(die))
            {
                currentColliding.Remove(die);
            }

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
       
    }
}
