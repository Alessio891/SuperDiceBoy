using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{

    public Cinemachine.CinemachineVirtualCamera Camera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController p = collision.gameObject.GetComponent<PlayerController>();
        if (p != null && PlayerController.SinglePlayer)
        {
            if (!Camera.enabled)
                Camera.enabled = true;
        }            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        PlayerController p = collision.gameObject.GetComponent<PlayerController>();
        if (p != null && PlayerController.SinglePlayer)
        {
            if (Camera.enabled)
                Camera.enabled = false;
        }
    }
}
