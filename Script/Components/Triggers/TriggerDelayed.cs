using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDelayed : ChamberTrigger
{
    [Header("IN SECONDS")]
    public float Delay;

    public void waitandtrigger()=>StartCoroutine(Trigger());

    IEnumerator Trigger()
    {
        yield return new WaitForSeconds(Delay);
        Activate();
    }
}
