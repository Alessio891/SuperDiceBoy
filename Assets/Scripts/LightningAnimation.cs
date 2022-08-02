using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAnimation : MonoBehaviour
{
    LineRenderer lr;
    Vector3 v1, v2;
    float _timer = 0;
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        v1 = lr.GetPosition(1);
        v2 = lr.GetPosition(2);
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 0.05f)
        {
            Vector3 newv1 = v1;
            Vector3 newv2 = v2;// + Random.Range(-2.0f, 2.0f);
            newv1.x += Random.Range(-2.0f, 2.0f);
            newv2.x += Random.Range(-2.0f, 2.0f);
            lr.SetPosition(1, newv1);
            lr.SetPosition(2, newv2);
            _timer = 0;
        }

    }
}
