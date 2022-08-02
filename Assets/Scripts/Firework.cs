using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    public float AliveTime = 1.5f;
    float _timer = 0;
    public ParticleSystem ps;
    bool exploded = false;
    public float Speed = 2.0f;

    public AudioClip Whistle, Explosion;
    public List<AudioClip> Explosions;

    AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    private void Start()
    {
        source.pitch = Random.Range(0.8f, 1.3f);
        source.PlayOneShot(Whistle);
        ps.Stop();
    }
    private void Update()
    {
        if (!exploded)
        {
            _timer += Time.deltaTime;
            if (_timer >= AliveTime)
            {
                exploded = true;
                _timer = 0;
                ps.Play();
                source.PlayOneShot(Explosions[Random.Range(0, Explosions.Count)]);
                Destroy(gameObject, 1.5f);
            }

            transform.position += Vector3.up * Speed * Time.deltaTime;
            transform.position += Vector3.right * Random.Range(-2.0f, 2.0f)*Time.deltaTime;
        }
    }

}
