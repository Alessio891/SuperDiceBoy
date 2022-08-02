using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransition : MonoBehaviour
{
    public static UITransition instance;
    System.Action OnTransitionInEnd;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        instance = this;
    }

    public void StartTransition(System.Action onTransitionEnd)
    {
        animator.SetTrigger("TransitionIn");
        OnTransitionInEnd = onTransitionEnd;
    }
    public void EndTransition()
    {
        animator.SetTrigger("TransitionOut");
        OnTransitionInEnd = null;
    }

    public void TransitionInEnd() {
        if (OnTransitionInEnd != null)
            OnTransitionInEnd();
    }
}
