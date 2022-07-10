using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamTrack : SiteScript<CamTrack>
{
    public Transform target;
    public float tracking;

    public bool zoom;

    public float MaxZoom;

    public Vector2 offset;

    Camera CamMain;

    private void Start() => CamMain = Camera.main;

    private void Update()
    {
        var mpos = CamMain.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        if (zoom)
        {
            if(Vector2.Distance(transform.position,mpos) < MaxZoom)
            {
                offset = (mpos-transform.position);
                return;
            }
            Vector2 direction = (CamMain.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position).normalized;
            offset = direction * MaxZoom;
            return;
        }
        offset = Vector2.zero;
    }

    void FixedUpdate()
    {
        if (!target) return;
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + offset.x, target.position.y + offset.y, -10), tracking);
    }
}
