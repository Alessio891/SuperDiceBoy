using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirbParticles : MonoBehaviour
{
    public float RangeReact = 10.0f;
    ParticleSystem ps;
    AudioSource s;
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        s = GetComponent<AudioSource>();
    }
    private void Start()
    {
        PlayerController.Player1.OnThrowDie += (d) =>
        {
            if (Vector3.Distance(PlayerController.Player1.transform.position, transform.position) < RangeReact)
            {
                if (d > 0.3f)
                {
                    ps.Play();
                    s.Play();
                }
            }
        };

        if (PlayerController.Player2 != null)
        {
            PlayerController.Player2.OnThrowDie += (d) =>
            {
                if (Vector3.Distance(PlayerController.Player2.transform.position, transform.position) < RangeReact)
                {
                    if (d > 0.3f)
                    {
                        ps.Play();
                        s.Play();
                    }
                }
            };
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, RangeReact);
    }
}
