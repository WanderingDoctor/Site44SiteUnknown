using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSwitch : InteractableTrigger
{
    void OnInteraction()
    {
        if (!OnRange) return;
        Activate();
    }
}
