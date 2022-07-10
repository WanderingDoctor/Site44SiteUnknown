using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollision : ChamberTrigger
{

    public int ToTrigger;

    public List<string> mask;

    public int uses;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (mask.Contains(collision.transform.tag) && uses > 0)
        {
            Activate();
            uses -= 1;
        }
    }
}
