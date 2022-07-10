using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorTrigger : TriggerPresence
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "LevelProp" || collision.transform.tag == "Character")
        {
        }
    }
}
