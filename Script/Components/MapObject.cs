using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapObject : ChamberProp
{
    Vector2 StartingPosition;

    Rigidbody2D rb2d;

    public bool ungrabable;

    public bool Destroyable;

    public float MaxHp;

    public float HP;

    public float MaxSpeed;

    public UnityEngine.Events.UnityEvent OnReset;

    public bool DestroyOnReset;

    [HideInInspector]public float StartingGrav;

    public override void Initialize()
    {
        HP = MaxHp;
        rb2d = GetComponent<Rigidbody2D>();
        StartingGrav = GetComponent<Rigidbody2D>().gravityScale;
        StartingPosition = transform.position;
    }

    public override void reset()
    {
        OnReset.Invoke();
        var player = Player.Instance;
        if (player && player.slot && player.slot.item)
        {
            player.slot.item.reset();
        }
        if (DestroyOnReset)
        {
            Destroy(gameObject);
            return;
        }
        HP = MaxHp;
        transform.position = StartingPosition;
        if (!rb2d)return;
        rb2d.velocity = Vector2.zero;
        rb2d.gravityScale = StartingGrav;
    }

    private void Update()
    {
        if (!Destroyable)return;    
        if (HP <= 0) reset();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!Destroyable) return;
        if (collision.relativeVelocity.magnitude > MaxSpeed)
        {
            HP -= (collision.relativeVelocity.magnitude-MaxSpeed)*.1f;
        }
    }
}
