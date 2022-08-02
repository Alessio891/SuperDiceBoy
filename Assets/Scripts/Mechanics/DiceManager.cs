using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public System.Action<DieController> OnDieExploded = (d) => { };

    public static DiceManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void DieExploded(DieController die)
    {
        OnDieExploded(die);
    }
}
