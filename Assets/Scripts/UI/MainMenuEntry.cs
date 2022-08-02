using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuEntry : MonoBehaviour, ISelectHandler, IDeselectHandler, ISubmitHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Transform SelectionPosition;
    public bool Clickable = true;
    public virtual void OnDeselect(BaseEventData eventData)
    {
        gameObject.LeanScale(Vector3.one, 0.2f).setEaseOutSine();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        OnDeselect(eventData);
    }

    public virtual void OnSelect(BaseEventData eventData)
    {
        MainMenuController.instance.PlaySelectSound();
        transform.localScale = Vector3.one;
        gameObject.LeanScale(new Vector3(1.05f, 1.05f, 1.05f), 0.2f).setEaseOutSine();
    }

    public virtual void OnSubmit(BaseEventData eventData)
    {
        if (Clickable)
            MainMenuController.instance.PlayClickSound();
    }
}
