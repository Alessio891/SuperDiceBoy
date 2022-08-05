using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MechanicDirection { Horizontal = 0, Vertical }
public class MovingPlatform : MonoBehaviour
{
    public float Speed = 5.0f;
    public float MoveRange = 10.0f;
    public bool changeDir = false;
    float _timer = 0;
    Vector3 startPos;
    public MechanicDirection Direction = MechanicDirection.Horizontal;
    MechanicDirection originalDir;
    public bool Continuos = true;
    bool Move = false;
    private void Start()
    {
        startPos = transform.position;
        originalDir = Direction;
    }
    private void Update()
    {
        if (Continuos || Move)
        {
            float dist = 0;// Mathf.Abs(startPos.x - transform.position.x);
            if (Direction == MechanicDirection.Horizontal)
            {
                transform.position += Vector3.left * Time.deltaTime * Speed * ((changeDir) ? -1 : 1);
               
                if (!changeDir)
                {
                    dist = startPos.x - transform.position.x;
                } else
                {
                    dist = transform.position.x - startPos.x;
                }
            }
            else if (Direction == MechanicDirection.Vertical)
            {
                transform.position += Vector3.up * Time.deltaTime * Speed * ((changeDir) ? -1 : 1);
                dist = Mathf.Abs(startPos.y - transform.position.y);
                if (changeDir)
                {
                    dist = startPos.y - transform.position.y;
                }
                else
                {
                    dist = transform.position.y - startPos.y;
                }
            }
            if (dist >= MoveRange)
            {
                changeDir = !changeDir;
                if (Move)
                    Move = false;
            }
        }
        
    }

    public void Reset()
    {
        transform.position = startPos;
        Direction = originalDir;
    }
    public void MovePlatform()
    {        
        Move = true;
    }
    public void ChangeDirection()
    {
        if (Direction == MechanicDirection.Horizontal)
            Direction = MechanicDirection.Vertical;
        else
            Direction = MechanicDirection.Horizontal;
    }

    private void OnDrawGizmos()
    {
        if (Direction == MechanicDirection.Horizontal)
            Gizmos.DrawWireCube(transform.position, new Vector3(MoveRange*2, 1.0f));
        else
            Gizmos.DrawWireCube(transform.position, new Vector3(1.0f, MoveRange * 2));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController p = collision.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            p.transform.SetParent(transform);
        } else
        {
            DieController d = collision.gameObject.GetComponent<DieController>();
            if (d != null && d.GetComponent<Rigidbody2D>().simulated && d.transform.parent.GetComponent<DoorSwitch>() == null)
            {
                d.transform.SetParent(transform);
            } else
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
                {
                    changeDir = !changeDir;
                    if (Move)
                        Move = false;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController p = collision.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            p.transform.SetParent(null);
        }
        else
        {
            DieController d = collision.gameObject.GetComponent<DieController>();
            if (d != null && d.GetComponent<Rigidbody2D>().simulated)
            {
                d.transform.SetParent(LevelManager.instance.CurrentLevelComponent.transform);
            }
        }
    }
}
