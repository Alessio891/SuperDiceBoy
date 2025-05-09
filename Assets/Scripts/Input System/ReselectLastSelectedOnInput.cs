﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

[RequireComponent(typeof(InputSystemUIInputModule))]
public class ReselectLastSelectedOnInput : MonoBehaviour
{
    private InputSystemUIInputModule standaloneInputModule;
    private GameObject lastSelectedObject;
    public static ReselectLastSelectedOnInput instance;
    void Awake()
    {
        instance = this;
        standaloneInputModule = GetComponent<InputSystemUIInputModule>();
    }
    void Update()
    {
        CacheLastSelectedObject();
        if (EventSystemHasObjectSelected())
            return;
        // If any axis/submit/cancel is pressed.
        // This looks at the input names defined in the attached StandaloneInputModule. You could use your own instead if you want.
        if (standaloneInputModule.move.action.triggered ||
                standaloneInputModule.submit.action.triggered ||
                standaloneInputModule.cancel.action.triggered)
        {
            // Reselect the cached 'lastSelectedObject'
            ReselectLastObject();
            return;
        }
    }
    // Called whenever a UI navigation/submit/cancel button is pressed.
    public static void ReselectLastObject()
    {
        // Do nothing if this is not active (maybe input objects were disabled)
        if (!instance.isActiveAndEnabled || !instance.gameObject.activeInHierarchy)
            return;
        // Otherwise we can proceed with setting the currently selected object to be 'lastSelectedObject'...

        // Current must be set to null first, otherwise it doesn't work properly because Unity UI is weird ¯\_(ツ)_/¯
        EventSystem.current.SetSelectedGameObject(null);

        // Set current to lastSelectedObject
        EventSystem.current.SetSelectedGameObject(instance.lastSelectedObject);
    }
    // Returns whether or not the EventSystem has anything selected
    static bool EventSystemHasObjectSelected()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
            return false;
        else
            return true;
    }
    // Caches last selected object for later use
    void CacheLastSelectedObject()
    {
        // Don't cache if nothing is selected
        if (EventSystemHasObjectSelected() == false)
            return;
        lastSelectedObject = EventSystem.current.currentSelectedGameObject.gameObject;
    }
}