using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour
{
    public float RotSpeed = 10.0f;
    float currentRot = 0;
    bool picked = false;
    public float SoundRange;
    private void Awake()
    {
    }
    private void Start()
    {        
    }

    private void Update()
    {
       //if (!picked)
         transform.Rotate(Vector3.forward, RotSpeed*Time.deltaTime);
        float dist = Vector3.Distance(transform.position, PlayerController.Player1.transform.position);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, SoundRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (picked)
            return;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            picked = true;
            Pickup();
        }
    }

    void Pickup() {
        picked = true;
        transform.LeanMoveY(transform.position.y + 2.0f, 2.5f).setEaseOutSine().setOnComplete( () => {
            transform.LeanRotateY(5200, 1.5f).setEaseInSine().setOnComplete(() => {
                transform.LeanMove(PlayerController.Player1.transform.position, 0.5f).setEaseOutSine();
                transform.LeanScale(Vector3.zero, 0.6f).setEaseOutSine().setOnComplete(() => Destroy(gameObject));
            });
        });

    }
}
