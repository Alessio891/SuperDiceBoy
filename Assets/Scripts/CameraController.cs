using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCam, CloserCamera;
    public CinemachineBrain Brain;
    public Transform GroupComposer;

    public float MinSize = 3.0f;
    public float MaxSize = 6.0f;
    public float MaxDist = 30.0f;
    public float MinDist = 10.0f;
    public static CameraController instance;
    private void Awake()
    {
        instance = this;
        Brain = GetComponent<CinemachineBrain>();
    }
    private void Start()
    {
        if (PlayerController.Player2 != null)
            VirtualCam.Follow = GroupComposer;
        else
            VirtualCam.Follow = PlayerController.Player1.transform;
    }
    public void Shake(float amount = 6.5f)
    {
        gameObject.LeanCancel();
        CinemachineBasicMultiChannelPerlin noise = VirtualCam.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = amount;
        gameObject.LeanValue((f) => noise.m_AmplitudeGain = f, amount, 0.0f, 0.3f).setEaseOutSine();
    }

    private void Update()
    {
        bool coop = PlayerController.Player2 != null;
/*
        if (coop)
        {
            float xDist = Mathf.Abs(PlayerController.Player1.transform.position.x - PlayerController.Player2.transform.position.x);
            float yDist = Mathf.Abs(PlayerController.Player1.transform.position.y - PlayerController.Player2.transform.position.y);

            float dist = xDist;
            if (yDist > xDist)
                dist = yDist;

            float distPerc =  (dist - MinDist) / (MaxDist-MinDist);
            if (distPerc > 1)
                distPerc = 1;
            if (distPerc < 0)
                distPerc = 0;

            

            float size = ((MaxSize - MinSize) * distPerc) + MinSize;

            Debug.Log("Cam dist perc: " + distPerc + " || Size: " + size);
            VirtualCam.m_Lens.OrthographicSize = size;
            
              

        }*/
    }

    public void ToggleCloser()
    {
        CloserCamera.enabled = !CloserCamera.enabled;
    }
}
