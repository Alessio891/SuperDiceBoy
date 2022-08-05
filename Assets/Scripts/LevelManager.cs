using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public AudioSource BGM;
    public float MaxBgmVolume = 0.3f;
    public List<LevelComponent> Levels;
    public static LevelManager instance;

    public List<Sprite> DieSides;

    int CurrentLevel = 0;
    
    public int StartLevel = -1;
    public PlayerInputManager input;
    public static bool FromMainMenu = false;
    public LevelComponent CurrentLevelComponent
    {
        get
        {
            return Levels[CurrentLevel];
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        foreach (LevelComponent c in Levels)
            c.gameObject.SetActive(false);
        if (FromMainMenu)
        {
            StartLevel = PlayerPrefs.GetInt("StartLevel", 0);
        }
        bool bgm = PlayerPrefs.GetInt("BGM", 1) == 1;
        if (!bgm)
        {
            BGM.Stop();
        } else
        {
            int volume = PlayerPrefs.GetInt("Volume", 5);
            float vol = MaxBgmVolume * (float)((float)volume / 10.0f);
            BGM.volume = vol;
        }
        LoadLevel(StartLevel,false);
    }

    public void LoadLevel(int level, bool transition = true) {
        Debug.Log("Loading level " + level);
        if (level < Levels.Count)
            StartCoroutine(LoadLevelRoutine(level, transition));
        else
            SceneManager.LoadScene(2);
    }    

    IEnumerator LoadLevelRoutine(int level, bool transition)
    {
        if (level < Levels.Count)
        {
            PlayerController.Player1.RemoveCarrying();
            if (PlayerController.Player2 != null)
                PlayerController.Player2.RemoveCarrying();

            if (transition)
            {
                bool transitionDone = false;
                UITransition.instance.StartTransition(() => transitionDone = true);
                while (!transitionDone)
                    yield return null;
            }
            Levels[CurrentLevel].gameObject.SetActive(false);
            Levels[level].gameObject.SetActive(true);
            PlayerController.Player1.transform.position = Levels[level].StartPos.position;

            if (PlayerController.Player2 != null)
            {
                PlayerController.Player2.transform.position = Levels[level].StartPos.position;
            }

            CurrentLevel = level;
            if (transition)
            {
                yield return new WaitForSeconds(0.4f);

                UITransition.instance.EndTransition();
            }
        }
    }
}
