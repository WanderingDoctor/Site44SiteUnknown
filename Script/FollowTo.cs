using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTo : MonoBehaviour
{
    public Transform Target;

    [Range(0,1)]public float Smoothie;

    public Vector2 Offset;

    Vector3 TargetPos;

    void Update()
    {
        TargetPos = new Vector3(Target.position.x, Target.position.y,transform.position.z);
    }

    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position,TargetPos) > .05f)
        {
            transform.position = Vector3.Lerp(transform.position,TargetPos,Smoothie);
            return;
        }
        transform.position = TargetPos;
    }
}
