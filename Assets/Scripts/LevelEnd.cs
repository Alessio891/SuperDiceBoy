using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public int NextLevel = 0;
    int current = 0;
    bool transition = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transition)
            return;
        int needed = (PlayerController.Player2 != null) ? 2 : 1;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")
            || collision.gameObject.layer == LayerMask.NameToLayer("Player 2"))
        {
            current++;
            if (current >= needed)
            {
                transition = true;
                PlayerPrefs.SetInt("Level_" + (NextLevel).ToString(), 1);
                LevelManager.instance.LoadLevel(NextLevel);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (transition)
            return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")
            || collision.gameObject.layer == LayerMask.NameToLayer("Player 2"))
        {
            current--;
        }
    }
}
