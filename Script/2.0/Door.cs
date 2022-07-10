using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Door : ChamberProp
{
    //Opacity of the Closed State of the Door
    [Range(0,1)]public float MinOpacity;
    public bool Open;

    float CurrentOpacity;

    Collider2D col;

    SpriteRenderer rend;

    void Start(){
        col = GetComponent<Collider2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    public override void Initialize()
    {
        if(Open){
            col.isTrigger = true;
            CurrentOpacity = MinOpacity;
            return;
        }
        col.isTrigger = false;
        CurrentOpacity = 1;
    }

    public override void reset(){
        
    }

    void Update(){
        if(rend.color.a == CurrentOpacity)return;
        rend.color = Color.Lerp(rend.color,new Color(rend.color.r,rend.color.g,rend.color.b,CurrentOpacity),1f);
    }

    public void ToggleDoor(){
        if(rend.color.a == 1){
            col.isTrigger = true;
            CurrentOpacity = MinOpacity;
            return;
        }
        col.isTrigger = false;
        CurrentOpacity = 1;
    }
}
