using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    CanvasGroup grp;

    public bool Open = false;
    public static GameMenu instance;    


    private void Awake()
    {
        instance = this;
        grp = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        Hide(true);
    }

    public void ResetLevel() {
        LevelManager.instance.CurrentLevelComponent.ResetLevel();
        Hide();
    }
    public void BackToMainMenu() {
        SceneManager.LoadScene(0);
    }
    public void ExitGame() { Application.Quit(); }

    public void Cancel()
    {
        Hide();
    }

    private void Update()
    {
     

    }

    public void Toggle(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) return;
        if (ctx.ReadValueAsButton())
        {
            if (Open)
                Hide();
            else
                Show();
        }
    }

    public void Show() {
        transform.localScale = new Vector3(1, 0, 1);
        grp.alpha = 1.0f;
        Open = true;
        transform.LeanScaleY(1, 0.2f).setEaseOutSine().setOnComplete(() => {
            
            grp.interactable = true;
            grp.blocksRaycasts = true;
        });
        EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
    }
    public void Hide(bool instant = false) {
        Open = false;
        if (!instant)
        {
            transform.LeanScaleY(0, 0.2f).setEaseOutSine().setOnComplete(() =>
            {
                grp.alpha = 0;
                grp.interactable = false;
                grp.blocksRaycasts = false;
            });
        } else
        {
            grp.alpha = 0;
            grp.interactable = false;
            grp.blocksRaycasts = false;
        }

    }
}
