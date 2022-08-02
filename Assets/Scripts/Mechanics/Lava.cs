using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Lava : MonoBehaviour
{
    SpriteShapeController shape;
    SpriteShapeRenderer r;

    public float TargetHeight = 1.0f;
    public float SpringConstant = 0.025f;
    public float Dampening = 0.025f;
    public float Spread = 0.5F;
    Vector3[] velocity;
    public AudioClip lavaSplash;
    public ParticleSystem LavaSplash;

    private void Awake()
    {
        r = GetComponent<SpriteShapeRenderer>();
        shape = GetComponent<SpriteShapeController>();
        velocity = new Vector3[10];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    float timer = -1;
    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer >= 1)
            timer = -1;

        Vector3 p = shape.spline.GetPosition(1);
        p.y += timer * 1.5f * Time.deltaTime;
//        shape.spline.SetPosition(1, p);

        for(int i = 1; i < 6; i++)
        {
            Vector3 pos = shape.spline.GetPosition(i);
            float x = pos.y - TargetHeight;
            float a = (-SpringConstant * x) - (Dampening*velocity[i].y) ;
            velocity[i].y += a;
            pos += velocity[i];
            shape.spline.SetPosition(i, pos);
        }
        float[] leftDeltas = new float[10];
        float[] rightDeltas = new float[10];

        for(int j = 0; j <8; j++)
        {
            for (int i = 1; i < 6; i++)
            {
                if (i > 0)
                {
                    leftDeltas[i] = Spread * (shape.spline.GetPosition(i).y - shape.spline.GetPosition(i - 1).y);
                    velocity[i - 1].y += leftDeltas[i];
                }
                if (i < 5)
                {
                    rightDeltas[i] = Spread * (shape.spline.GetPosition(i).y - shape.spline.GetPosition(i + 1).y);
                    velocity[i+1].y += rightDeltas[i];
                }
            }
            for(int i = 1; i < 6; i++)
            {
                if (i > 1)
                {
                    Vector3 pos = shape.spline.GetPosition(i-1);
                    pos.y += leftDeltas[i];
                    shape.spline.SetPosition(i - 1, pos);
                }
                if (i < 5)
                {
                    Vector3 pos = shape.spline.GetPosition(i + 1);
                    pos.y += rightDeltas[i];
                    shape.spline.SetPosition(i + 1, pos);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ParticleSystem ps = GameObject.Instantiate<ParticleSystem>(LavaSplash);
        ps.transform.position = collision.gameObject.transform.position;
        AudioSource s = collision.gameObject.GetComponent<AudioSource>();
        if (s != null)
            s.PlayOneShot(lavaSplash);
        DieController d = collision.gameObject.GetComponent<DieController>();
        if (d != null)
            d.DestroyAndRespawn(true);
        PlayerController p = collision.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            p.ResetToLastPosition();
        }
    }
}
