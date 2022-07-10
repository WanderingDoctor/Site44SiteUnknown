using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PropManipulator : Item
{
    GameObject grabbed;

    Rigidbody2D grabbedRigidbody;

    Player player;

    Camera CamMain;

    public float HoldDistance;
    public float GrabDistance;

    public LayerMask layermask;
    public float LaunchForce;

    public override void UsePrimary()
    {
        if (!grabbed && !grabbedRigidbody) return;
        Vector2 mpos = CamMain.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        var dir = (mpos - grabbedRigidbody.position).normalized;
        grabbedRigidbody.AddForce(dir * LaunchForce, ForceMode2D.Impulse);
        ResetGrabbed();
    }

    public override void UseSecondary()
    {
        if (!grabbed && !grabbedRigidbody)
        {
            if (!player.LookingProp || !player.LookingProp.GetComponent<MapObject>() || !player.LookingProp.GetComponent<MapObject>().isActiveAndEnabled 
                || player.LookingProp.GetComponent<MapObject>().ungrabable || Vector2.Distance(player.Head.position, player.LookingProp.position) > GrabDistance) return;
            grabbed = player.LookingProp.gameObject;
            grabbedRigidbody = grabbed.GetComponent<Rigidbody2D>();
            grabbed.gameObject.layer = 12;
            grabbed.transform.tag = "grabbed";
            return;
        }
        ResetGrabbed();
    }

    public override void ItemFixedUpdate()
    {
        if (!player) return;
        if (!grabbed && !grabbedRigidbody)return;

        grabbedRigidbody.gravityScale = 0;
        grabbedRigidbody.angularVelocity = 0;
        grabbedRigidbody.velocity = Vector2.zero;

        var mpos = CamMain.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        var dir = ((Vector2)mpos - grabbedRigidbody.position).normalized;
        var hit = Physics2D.Raycast(grabbedRigidbody.position, dir, .75f, layermask);

        if (!hit)
        {
            grabbedRigidbody.position = Vector2.Lerp(grabbedRigidbody.position,player.Head.position+player.Head.right*HoldDistance, .25f);
            return;
        }
        grabbedRigidbody.AddForce(-dir * 50);
    }

    public override void ItemUpdate()
    {
    }

    public override void ItemStart()
    {
        player = Player.Instance;
        CamMain = Camera.main;
    }

    public override void reset()
    {
        if (!player) return;
        ResetGrabbed();
    }

    public override void ItemDrop()
    {
        ResetGrabbed();
        player = null;
    }

    void ResetGrabbed()
    {
        if (!grabbed && !grabbedRigidbody) return;
        grabbed.transform.tag = "LevelProp";
        grabbedRigidbody.gravityScale = grabbed.GetComponent<MapObject>().StartingGrav;
        grabbed.gameObject.layer = 10;
        grabbed = null;
        grabbedRigidbody = null;
    }
}
