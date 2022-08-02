using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            MainMenuEntry e = EventSystem.current.currentSelectedGameObject.GetComponent<MainMenuEntry>();
            if (e != null)
            {
                GetComponent<Image>().enabled = true;
                transform.position = e.SelectionPosition.position;
            } 
        } else
        {
            GetComponent<Image>().enabled = false;
        }
    }
}
