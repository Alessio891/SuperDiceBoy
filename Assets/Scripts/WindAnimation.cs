using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.8f, 1, 1);
        transform.LeanScaleX(1.2f, 1.5f).setLoopPingPong().setEaseOutSine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
