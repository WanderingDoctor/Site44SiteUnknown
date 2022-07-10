using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class MovingPlataform : ChamberProp
{
    public Transform EndPosobj;
    public Vector3 StartPos, EndPos, nextPos;

    public enum OnStart {Nothing,Open,Close }public OnStart onStart;

                                //Start/Finish//
    public enum Directional {None,BottomTop,TopBottom,LeftRight,RightLeft}public Directional directional;

    public bool StandOn;

    public bool Move;

    public float Speed;

    public List<string> TagsAccepted;

    Vector3 Curpos;

    public UnityEvent OnPositionReach;

    void Awake()
    {
        StartPos = new Vector3(transform.position.x, transform.position.y,transform.position.z);
        EndPos = new Vector3(EndPosobj.position.x, EndPosobj.position.y, transform.position.z);
    }

    private void Update()
    {
        EndPos = new Vector3(EndPosobj.position.x, EndPosobj.position.y, transform.position.z);
        Curpos = new Vector3(transform.position.x, transform.position.y,transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (StandOn && TagsAccepted.Contains(collision.transform.tag))
        {
            collision.transform.parent = transform;
            collision.transform.rotation = collision.transform.rotation;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (StandOn)
        {
            if (collision.transform.tag == "LevelProp")
            {
                collision.transform.parent = null;
                return;
            }
            if (collision.transform.tag == "Player")
            {
                collision.transform.parent = GameObject.FindGameObjectWithTag("StartRoom").transform;
            }
        }
    }

    public override void Initialize()
    {
        switch (onStart)
        {
            case OnStart.Close:
                Close();
                break;
            case OnStart.Open:
                Open();
                break;
            default:
                break;
        }
    }

    public override void reset()
    {
        Close();
    }

    public void Toggle()
    {
        Move = !Move;
    }

    public void ToggleNextpos()
    {
        if(nextPos == EndPos)
        {
            nextPos = StartPos;
            return;
        }
        nextPos = EndPos;
    }

    public void Open()
    {
        nextPos = EndPos;
    }

    public void Close()
    {
        nextPos = StartPos;
    }
    private void FixedUpdate()
    {
        if (nextPos != Vector3.zero && Move)
        {
            if (Vector2.Distance(Curpos, nextPos) > .05f )
            {
                transform.Translate((nextPos - Curpos).normalized * Speed, Space.World);
                return;
            }
            transform.position = nextPos;
            OnPositionReach.Invoke();
        }
    }
}
