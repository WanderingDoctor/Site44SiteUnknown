using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Item :  MonoBehaviour
{
    public Sprite SpriteOnPlayer;
    public Sprite SpriteOutOfPlayer;

    public abstract void UsePrimary();
    public abstract void UseSecondary();
    public abstract void reset();
    public abstract void ItemStart();
    public abstract void ItemUpdate();
    public abstract void ItemFixedUpdate();
    public abstract void ItemDrop();
}
