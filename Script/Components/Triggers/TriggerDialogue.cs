using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDialogue : ChamberTrigger
{
    [TextArea(0,32)]public List<string> TextLines;

    HUD hud;

    private void Awake()
    {
        hud = HUD.Instance;
    }

    public void DisplayDialogue()=>StartCoroutine(TextDisplay());

    IEnumerator TextDisplay()
    {
        foreach (var textline in TextLines)
        {
            hud.Subtitle.text = textline;
            yield return new WaitForSeconds(textline.Length * .0575f);
        }
        Activate();
        hud.Subtitle.text = "";
    }
}
