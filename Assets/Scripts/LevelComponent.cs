using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelComponent : MonoBehaviour
{
    public int LevelIndex = 0;
    public Transform StartPos;
    public List<DieController> Dice;
    public Transform BirdPositions;
    public List<Transform> BirdPositionList;
    public Transform RandomBirdPosition
    {
        get
        {
            return BirdPositionList[Random.Range(0, BirdPositionList.Count)];
        }
    }
    private void Awake()
    {
        Dice = GetComponentsInChildren<DieController>().ToList();
        if (BirdPositions != null)
        {
            BirdPositionList = new List<Transform>();
            for (int i = 0; i < BirdPositions.childCount; i++)
                BirdPositionList.Add(BirdPositions.GetChild(i).transform);
        }

    }

    public void ResetLevel()
    {

        if (PlayerController.Player1.Carrying && PlayerController.Player1.nearDice != null)
        {
            PlayerController.Player1.nearDice.transform.SetParent(transform);
            PlayerController.Player1.nearDice.GetComponent<Rigidbody2D>().simulated = true;
            PlayerController.Player1.Carrying = false;
            PlayerController.Player1.nearDice = null;
        }

        foreach(DieController d in GetComponentsInChildren<DieController>())
        {
            if (!d.DoNotReset)
            {
                d.transform.position = d.originalPos;
                d.Thrown = 0;
            }
        }
        foreach(MovingPlatform p in GetComponentsInChildren<MovingPlatform>())
        {
            p.Reset();
        }
        PlayerController.Player1.transform.position = StartPos.position;
        if (PlayerController.Player2 != null)
        {
            PlayerController.Player2.transform.position = StartPos.position;
            if (PlayerController.Player2.Carrying && PlayerController.Player2.nearDice != null)
            {
                PlayerController.Player2.nearDice.transform.SetParent(transform);
                PlayerController.Player2.nearDice.GetComponent<Rigidbody2D>().simulated = true;
                PlayerController.Player2.Carrying = false;
                PlayerController.Player2.nearDice = null;
            }
        }
        foreach(Door d in GetComponentsInChildren<Door>())
        {
            d.Close();
        }
    }
}
