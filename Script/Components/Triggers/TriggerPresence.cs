using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerPresence : ChamberTrigger
{
    public List<string> tags;

    public int uses;

    public Transform Detected;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Detected == null)
        {
            if (tags.Contains(collision.transform.tag) && uses > 0)
            {
                Activate();
                Detected = collision.transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform == Detected)
        {
            if (uses > 0)
            {
                uses--;
                Detected = null;
            }
        }
    }
}
