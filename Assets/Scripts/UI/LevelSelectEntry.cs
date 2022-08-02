using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectEntry : MainMenuEntry
{
    public int LevelIndex => transform.GetSiblingIndex();
    public GameObject LockedStatus;
    public Text LevelNumber, TrophyNumber;
    public bool Unlocked = false;

    private void Start()
    {
        LevelNumber.text = "Level " + (LevelIndex + 1).ToString();
        Refresh();
    }
    public void SelectThis()
    {
        if (Unlocked)
            MainMenuController.instance.LoadNewScene(LevelIndex);
    }

    public void Refresh()
    {
        if (LevelIndex > 0)
        {
            int unlocked = PlayerPrefs.GetInt("Level_" + LevelIndex.ToString(), 0);
            Unlocked = (unlocked != 0);
        }
        else
            Unlocked = true;
        LockedStatus.SetActive(!Unlocked);
    }
}
