using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Image Separator, Title;
    public ParticleSystem PS;
    public CanvasGroup Menu, OptionsCanvas, CreditsCanvas, StartGameMenu, CursorImg;
    public GameObject CodeActivateObj;
    public Text VolumeValue, BgmValue, ResolutionValue, FullscreenValue;
    int resolutionIndex = 0;

    public float MaxBGMVolume = 0.2f;

    public AudioSource BGM, Sfx;

    public AudioClip UISelectSfx, UIClickSfx;

    Player input;

    bool inOptions = false;

    public List<Key> ResetSequence;
    int currentSequenceIndex = 0;

    public List<Key> UnlockSequence;
    int currentUnlockSequenceIndex = 0;

    public static MainMenuController instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        input = new Player();
        LoadSavedSettings();
        StartCoroutine(StartAnimation());
        CursorImg.alpha = 0;

    }

    public void PlaySelectSound() {
        Sfx.PlayOneShot(UISelectSfx);
    }
    public void PlayClickSound() {
        Sfx.PlayOneShot(UIClickSfx);
    }

    IEnumerator StartAnimation()
    {
        OptionsCanvas.alpha = 0;
        OptionsCanvas.interactable = false;
        OptionsCanvas.blocksRaycasts = false;
        Menu.alpha = 0;
        Menu.interactable = false;
        PS.Pause();
        Separator.transform.localScale = new Vector3(0, Separator.transform.localScale.y, 0);
        Title.transform.localScale = Vector3.zero;

        Title.gameObject.LeanScale(Vector3.one, 1.5f).setEaseOutSine();
        yield return new WaitForSeconds(1.5f);
        Separator.gameObject.LeanScale(Vector3.one, 0.3f).setEaseOutQuad();
        gameObject.LeanValue((f) => Menu.alpha = f, 0.0f, 1.0f, 1.5f).setEaseOutSine().setOnComplete(() => { 
            Menu.interactable = true;
            Menu.blocksRaycasts = true;
            Menu.transform.GetChild(0).GetComponent<Button>().Select();
            CursorImg.alpha = 1;
        });
        PS.Play();
    }
    float h = 0;
    public void MovementInput(InputAction.CallbackContext ctx)
    {
        if (inOptions && ctx.started)
            h = ctx.ReadValue<float>();
    }
    public void CancelBtn(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValueAsButton() && !ctx.performed)
        {
            if (inOptions)
                CloseOptions();
            else if (CreditsCanvas.alpha > 0)
                CloseCredits();
        }
    }
    private void Update()
    {
        Cursor.visible = true;


        //Keyboard.current[ResetSequence[currentSequenceIndex]].wasPressedThisFrame

        if (Keyboard.current[ResetSequence[currentSequenceIndex]].wasPressedThisFrame)
        {
            if (++currentSequenceIndex == ResetSequence.Count)
            {
                for(int i = 0; i < 15; i++)
                {
                    PlayerPrefs.SetInt("Level_" + i.ToString(),0);
                }
                foreach (LevelSelectEntry e in StartGameMenu.GetComponentsInChildren<LevelSelectEntry>())
                    e.Refresh();

                currentSequenceIndex = 0;
                CodeActivateObj.GetComponent<Animator>().SetTrigger("Animate");
                Debug.Log("Level unlock reset");
            }
        } else if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            currentSequenceIndex = 0;
        }
        if (Keyboard.current[UnlockSequence[currentUnlockSequenceIndex]].wasPressedThisFrame)
        {
            if (++currentUnlockSequenceIndex == UnlockSequence.Count)
            {
                for (int i = 0; i < 15; i++)
                {
                    PlayerPrefs.SetInt("Level_" + i.ToString(), 1);
                }
                foreach (LevelSelectEntry e in StartGameMenu.GetComponentsInChildren<LevelSelectEntry>())
                    e.Refresh();

                currentUnlockSequenceIndex = 0;
                Debug.Log("Level unlock");
                CodeActivateObj.GetComponent<Animator>().SetTrigger("Animate");
            }
        }
        else if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            currentUnlockSequenceIndex = 0;
        }

        if (inOptions)
        {            
            GameObject sel = EventSystem.current.currentSelectedGameObject;
            if (sel == null)
                return;
            int index = sel.transform.GetSiblingIndex();
            Debug.Log("Options index " + index +  " || H: " + h);
            switch(index)
            {
                case 0:
                    if (h > 0)
                        VolumeUp();
                    else if (h < 0)
                        VolumeDown();
                    break;
                
                case 2:
                    if (h > 0)
                    {
                        NextResolution();
                    }
                    else if (h < 0)
                        PreviousResolution();
                    break;               
            }
            h = 0;
        }

    }

    public void StartSingle()
    {
        PlayerPrefs.SetInt("Coop", 0);
        StartGame();

    }
    public void StartMulti() {
        PlayerPrefs.SetInt("Coop", 1);
        StartGame();
    }
    public void CloseStartGame()
    {
        
        gameObject.LeanValue((f) => StartGameMenu.alpha = f, 1.0f, 0.0f, 0.4f).setEaseOutSine().setOnComplete(() => {
            StartGameMenu.blocksRaycasts = false;
            StartGameMenu.interactable = false;
            gameObject.LeanValue((f) => Menu.alpha = f, 0.0f, 1.0f, 0.4f).setEaseOutSine().setOnComplete(() =>
            {
                Menu.interactable = true;
                Menu.blocksRaycasts = true;
                Menu.transform.GetChild(0).GetComponent<Button>().Select();
            });
        });
    }
    public void StartGame() {
        gameObject.LeanValue((f) => Menu.alpha = f, 1.0f, 0.0f, 0.4f).setEaseOutSine().setOnComplete(() => {
            Menu.blocksRaycasts = false;
            Menu.interactable = false;
            gameObject.LeanValue((f) => StartGameMenu.alpha = f, 0.0f, 1.0f, 0.4f).setEaseOutSine().setOnComplete(() =>
            {
                StartGameMenu.interactable = true;
                StartGameMenu.blocksRaycasts = true;
                StartGameMenu.transform.GetChild(0).transform.GetChild(0).GetComponent<Button>().Select();
            });
        });
    }
    public void LoadNewScene(int levelIndex)
    {
        PlayerPrefs.SetInt("StartLevel", levelIndex);
        SceneManager.LoadScene(1);
    }
    public void Options() {
        EventSystem.current.SetSelectedGameObject(null);
        CursorImg.alpha = 0;
        gameObject.LeanValue((f) => Menu.alpha = f, 1.0f, 0.0f, 0.4f).setEaseOutSine().setOnComplete(() => {
            Menu.blocksRaycasts = false;
            Menu.interactable = false;
            gameObject.LeanValue((f) => OptionsCanvas.alpha = f, 0.0f, 1.0f, 0.4f).setEaseOutSine().setOnComplete(() =>
            {
                inOptions = true;
                OptionsCanvas.interactable = true;
                OptionsCanvas.blocksRaycasts = true;
                OptionsCanvas.transform.GetChild(0).GetComponent<Button>().Select();
                CursorImg.alpha = 1;
            });
        });
    }
    public void CloseOptions()
    {
        EventSystem.current.SetSelectedGameObject(null);
        CursorImg.alpha = 0;
        gameObject.LeanValue((f) => OptionsCanvas.alpha = f, 1.0f, 0.0f, 0.4f).setEaseOutSine().setOnComplete(() => {
            OptionsCanvas.blocksRaycasts = false;
            OptionsCanvas.interactable = false;
            gameObject.LeanValue((f) => Menu.alpha = f, 0.0f, 1.0f, 0.4f).setEaseOutSine().setOnComplete(() =>
            {

                inOptions = false;
                Menu.interactable = true;
                Menu.blocksRaycasts = true;
                Menu.transform.GetChild(0).GetComponent<Button>().Select();
                CursorImg.alpha = 1;
            });
        });
    }
    public void Credits() {
        EventSystem.current.SetSelectedGameObject(null);
        CursorImg.alpha = 0;
        gameObject.LeanValue((f) => Menu.alpha = f, 1.0f, 0.0f, 0.4f).setEaseOutSine().setOnComplete(() => {
            Menu.blocksRaycasts = false;
            Menu.interactable = false;
            gameObject.LeanValue((f) => CreditsCanvas.alpha = f, 0.0f, 1.0f, 0.4f).setEaseOutSine().setOnComplete(() =>
            {
                CreditsCanvas.interactable = true;
                CreditsCanvas.blocksRaycasts = true;
                CursorImg.alpha = 1;
            });
        });
    }
    public void CloseCredits()
    {
        EventSystem.current.SetSelectedGameObject(null);
        CursorImg.alpha = 0;
        gameObject.LeanValue((f) => CreditsCanvas.alpha = f, 1.0f, 0.0f, 0.4f).setEaseOutSine().setOnComplete(() => {
            CreditsCanvas.blocksRaycasts = false;
            CreditsCanvas.interactable = false;
            gameObject.LeanValue((f) => Menu.alpha = f, 0.0f, 1.0f, 0.4f).setEaseOutSine().setOnComplete(() =>
            {
                Menu.interactable = true;
                Menu.blocksRaycasts = true;
                CursorImg.alpha = 1;
            });
        });
    }
    void LoadSavedSettings()
    {
        int volume = PlayerPrefs.GetInt("Volume", 5);
        bool bgm = PlayerPrefs.GetInt("BGM", 1) == 1;
        int w = PlayerPrefs.GetInt("ScreenW", Screen.currentResolution.width);
        int h = PlayerPrefs.GetInt("ScreenH", Screen.currentResolution.height);
        bool fullScreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;


        resolutionIndex = GetResolutionIndex(w, h);

        ResolutionValue.text = w.ToString() + "x" + h.ToString();
        VolumeValue.text = volume.ToString();
        BgmValue.text = (bgm) ? "Enabled" : "Disabled";
        FullscreenValue.text = (fullScreen) ? "Yes" : "No";
        Sfx.volume = MaxBGMVolume * (float)((float)volume / 10.0f); 
        if (bgm)
        {
            BGM.volume = MaxBGMVolume * (float)((float)volume / 10.0f);
        } else
        {
            BGM.Stop();
        }
    }

    int GetResolutionIndex(int w, int h)
    {
        for(int i = 0; i < Screen.resolutions.Length;i++)
        {
            if (Screen.resolutions[i].width == w && Screen.resolutions[i].height == h)
                return i;
        }
        return 0;
    }

    public void VolumeUp() {
        int volume = PlayerPrefs.GetInt("Volume", 5);
        if (volume < 10)
        {
            volume++;
            BGM.volume = MaxBGMVolume * (float)((float)volume / 10.0f);
            Sfx.volume = BGM.volume;
            PlayerPrefs.SetInt("Volume", volume);
            VolumeValue.text = volume.ToString();
        }
    }
    public void VolumeDown() {
        int volume = PlayerPrefs.GetInt("Volume", 5);
        if (volume > 0)
        {
            volume--;
            BGM.volume = MaxBGMVolume * (float)((float)volume / 10.0f);
            Sfx.volume = BGM.volume;
            PlayerPrefs.SetInt("Volume", volume);
            VolumeValue.text = volume.ToString();
        }
    }

    public void ToggleBGM() {
        bool bgm = PlayerPrefs.GetInt("BGM", 1) == 1;
        bgm = !bgm;
        BgmValue.text = (bgm) ? "Enabled" : "Disabled";
        if (!bgm)
            BGM.Stop();
        else
            BGM.Play();
        PlayerPrefs.SetInt("BGM", (bgm) ? 1 : 0);
    }

    public void NextResolution() {
        resolutionIndex++;
        if (resolutionIndex >= Screen.resolutions.Length)
            resolutionIndex = 0;
        PlayerPrefs.SetInt("ScreenW", Screen.resolutions[resolutionIndex].width);
        PlayerPrefs.SetInt("ScreenH", Screen.resolutions[resolutionIndex].height);
        Screen.SetResolution(Screen.resolutions[resolutionIndex].width, Screen.resolutions[resolutionIndex].height, Screen.fullScreen);
        ResolutionValue.text = Screen.resolutions[resolutionIndex].width.ToString() + "x" + Screen.resolutions[resolutionIndex].height.ToString();
    }
    public void PreviousResolution() {
        resolutionIndex--;
        if (resolutionIndex < 0)
            resolutionIndex = Screen.resolutions.Length-1;
        PlayerPrefs.SetInt("ScreenW", Screen.resolutions[resolutionIndex].width);
        PlayerPrefs.SetInt("ScreenH", Screen.resolutions[resolutionIndex].height);
        Screen.SetResolution(Screen.resolutions[resolutionIndex].width, Screen.resolutions[resolutionIndex].height, Screen.fullScreen);
        ResolutionValue.text = Screen.resolutions[resolutionIndex].width.ToString() + "x" + Screen.resolutions[resolutionIndex].height.ToString();
    }

    public void ToggleFullscreen() {
        bool fullScreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        fullScreen = !fullScreen;

        PlayerPrefs.SetInt("Fullscreen", (fullScreen) ? 1 : 0);
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, fullScreen);

        FullscreenValue.text = (fullScreen) ? "Yes" : "No";
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
