using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : ChamberProp
{
    public Item Holding;

    SpriteRenderer rend;

    public Sprite NoItemSprite;

    public void Pickup()
    {
        var player = Player.Instance;
        if (player.slot)
        {
            var temp = player.slot.item;
            player.slot.item = Holding;
            Holding = temp;
            if (!player.slot.item && Holding)
            {
                Holding.ItemDrop();
                return;
            }
            player.slot.item.ItemStart();
        }
    }

    public override void Initialize()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    public override void reset()
    {
    }

    void Update()
    {
        if (Holding)
        {
            rend.sprite = Holding.SpriteOutOfPlayer;
            return;
        }
        rend.sprite = NoItemSprite;
    }
}
