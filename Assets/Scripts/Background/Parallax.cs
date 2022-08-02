using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform target;

    Vector2 startPosition;
    float startZ;
    Vector2 travel => (Vector2)Camera.main.transform.position - startPosition;
    float distanceFromTarget => transform.position.z - target.position.z;
    public bool LockY = true;
    float clippingPlane => (Camera.main.transform.position.z + (distanceFromTarget > 0 ? Camera.main.farClipPlane : Camera.main.nearClipPlane));
    public float parallaxFactor = 1.0f;// Mathf.Abs(distanceFromTarget) / clippingPlane;
    private void Start()
    {
        startPosition = transform.position;
        startZ = transform.position.z;
    }

    private void FixedUpdate()
    {

        Vector2 newPos = startPosition + travel * parallaxFactor;
        transform.position = Vector3.Lerp(transform.position, new Vector3(newPos.x, (LockY) ? transform.position.y : newPos.y, startZ), Time.fixedDeltaTime * 235.5f);
    }
}
