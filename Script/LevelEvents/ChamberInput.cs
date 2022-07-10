using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class ChamberInput : ChamberTrigger
{
    //Text displayed on subtitles
    [TextArea(0,32)]public string ToDisplay;

    //Input waited
    InputActionReference waitfor;

    //Index of input waited
    public int index;

    //Subtitle handler
    HUD hud;

    private void Awake()
    {
        hud = HUD.Instance;
        StartCoroutine(WaitForInput(waitfor));
    }

    //Waits for correct input to be pressed
    IEnumerator WaitForInput(InputActionReference input)
    {
        while (!Keyboard.current.FindKeyOnCurrentKeyboardLayout(input.action.bindings[index].ToDisplayString()).isPressed)
        {
            hud.Subtitle.text = $"Press {input.name} To {ToDisplay}";
            yield return null;
        }
        Activate();
        hud.Subtitle.text = "";
    }
}
