using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : SiteScript<Player>
{
    CamTrack tracking;

    Camera CamMain;

    public Transform SpawnPoint;

    public Transform Head;

    public PlayerInput Inputs;

    public ItemSlot slot;

    public LevelManager levelManager;

    public Rigidbody2D rb2d;

    public bool
        IsGrounded;

    int X;

    [HideInInspector]public PropController controller;

    public List<Transform> mousetrack;

    public Vector2 Jump;

    public float
        AirStrafe,
        RunningMulti,
        CrouchMulti;

    float run = 1;
    float crouch = 1;

    public float MovementSpeed;

    public bool
        NoControl,
        Alive;

    public Transform LookingProp;

    public List<InteractableTrigger> Nearby;

    private void Awake()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        tracking = CamTrack.Instance;
        CamMain = Camera.main;
    }

    void Start()
    {
        if(SpawnPoint)transform.position = SpawnPoint.position;
        Alive = true;
    }

    void Update()
    {
        if (Alive == false)
        {
            NoControl = true;
            transform.position = SpawnPoint.position;
            NoControl = false;
            Alive = true;
            if (slot.item) slot.item.reset();
            return;
        }
        if (!NoControl)
        {
            if (controller)Inputs.SwitchCurrentActionMap("Controller");
            if (slot && slot.item)
            {
                slot.item.ItemUpdate();
            }
            return;
        }
        X = 0;     
    }

    private void FixedUpdate()
    {
        if (NoControl || !rb2d) return;
        if (slot && slot.item)
        {
            slot.item.ItemFixedUpdate();
        }
        var mpos = CamMain.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        var hit = Physics2D.RaycastAll(Head.position, Head.right, Vector2.Distance(Head.position, mpos), LayerMask.GetMask("Prop", "Misc"));
        Debug.DrawRay(Head.position, Head.right * Vector2.Distance(Head.position, mpos));
        if (hit.Length>0) LookingProp = hit[0].transform; 
        else LookingProp = null;
        if(crouch != 1)
        {
            rb2d.AddForce(-transform.up * 50);
        }
        foreach (var obj in mousetrack)
        {
            Vector3 difference = mpos - obj.position;
            difference.Normalize();
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            var final = Quaternion.Euler(0f, 0f, rotation_z);
            obj.rotation = final; 
        }
        if (IsGrounded)
        {
            rb2d.velocity = Vector2.Lerp(rb2d.velocity, transform.right * (MovementSpeed * X) * run * crouch, .3f);
            return;
        }
        rb2d.velocity += new Vector2(AirStrafe * X, 0);
    }

    void OnItemUsePri()
    {
        if (NoControl || !slot || !slot.item) return;
        slot.item.UsePrimary();
    }

    void OnItemUseSec()
    {
        if (NoControl || !slot || !slot.item) return;
        slot.item.UseSecondary();
    }

    void OnInteraction()
    {
        Nearby.ForEach(a=>a.SendMessage("OnInteraction"));
    }

    void OnMove(InputValue value)
    {
        if (NoControl) return;
        X = (int)value.Get<Vector2>().normalized.x;
    }

    void OnRun(InputValue value)
    {
        if (NoControl) return;
        if (value.isPressed)
        {
            run = RunningMulti;
            return;
        }
        run = 1;
    }

    void OnJump()
    {
        if (!IsGrounded || NoControl) return;
        if (X > 0)
        {
            rb2d.velocity += new Vector2(Jump.x * X, Jump.y - ((Jump.x * X) * .5f));
            IsGrounded = false;
            return;
        }
        else if (X < 0)
        {
            rb2d.velocity += new Vector2(Jump.x * X, Jump.y + ((Jump.x * X) * .75f));
            IsGrounded = false;
            return;
        }
        rb2d.velocity += new Vector2(0, Jump.y);
        IsGrounded = false;
    }

    void OnZoom(InputValue value)
    {
        if (NoControl)
        {
            return;
        }
        tracking.zoom = value.isPressed;
        return;
    }

    void OnCrouch(InputValue value)
    {
        if (NoControl) return;
        if (value.isPressed)
        {
            crouch = CrouchMulti;
            //transform.localScale = Vector3.one * .6f;
            return;
        }
        crouch = 1;
        //transform.localScale = Vector3.one;
    }

    void OnControllerExit()=>controller.ToggleControl();
    
    void OnControllerUp(InputValue value)=>controller.ControllerUp(value);

    void OnControllerDown(InputValue value) =>controller.ControllerDown(value);

    void OnControllerLeft(InputValue value) =>controller.ControllerLeft(value);

    void OnControllerRight(InputValue value) =>controller.ControllerRight(value);

    void OnControllerSwitchUp() =>controller.ControllerSwitchUp();

    void OnControllerSwitchDown() =>controller.ControllerSwitchDown();
}
