using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemSlot : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public UnityEvent OnUse, OnAltUse, OnReset;

    public Item item;

    private void Start()=>spriteRenderer = GetComponent<SpriteRenderer>();

    private void Update()
    {
        if (item)
        {
            spriteRenderer.sprite = item.SpriteOnPlayer;
            return;
        }
        spriteRenderer.sprite = null;
    }
}
