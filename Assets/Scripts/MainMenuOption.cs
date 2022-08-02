using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    public UnityEvent OnClick;
    
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.LeanScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.LeanScale(Vector3.one, 0.2f);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        OnClick.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (OnClick == null)
            OnClick = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
