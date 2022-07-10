using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractableTrigger : ChamberTrigger
{
    [Space]
    public bool OnRange;

    Camera CamMain;

    Player player;

    private void Start(){
        CamMain =Camera.main;
        player = Player.Instance;
    }

    void OnGUI()
    {
        if (!player)
        {
            player = Player.Instance;
            return;
        }
        var playerpos = CamMain.WorldToScreenPoint(player.transform.position);
        if (OnRange)
        {
            GUI.Label(new Rect(playerpos.x-1.5f, playerpos.y, Screen.width, Screen.height), $"<color=black><size=15><b>Press {player.Inputs.actions["Player/Interaction"].bindings[0].ToDisplayString()} To Interact</b></size></color>");
            GUI.Label(new Rect(playerpos.x, playerpos.y, Screen.width, Screen.height), $"<color=white><size=15><b>Press {player.Inputs.actions["Player/Interaction"].bindings[0].ToDisplayString()} To Interact</b></size></color>");
        }
    }

    void Update()
    {
        if (!player)
        {
            player = Player.Instance;
            return;
        }
        if(OnRange && !player.Nearby.Contains(this))
        {
            player.Nearby.Add(this);
            return;
        }
        if (!OnRange && player.Nearby.Contains(this)) player.Nearby.Remove(this);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject) OnRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject) OnRange = false;
    }
}
