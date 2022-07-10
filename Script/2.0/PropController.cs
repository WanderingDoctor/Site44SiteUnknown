using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PropController : MonoBehaviour
{
    public List<ChamberProp> Props;

    public Transform CamPosition;

    CamTrack tracking;

    Camera CamMain;

    ChamberProp Selected;

    Player player;

    private void Start()
    {
        CamMain = Camera.main;
        tracking = CamTrack.Instance;
    }

    private void Update()
    {
        if (!player) return;
        if (Props.Count == 0) return;
        if (!Selected)
        {
            Selected = Props[0];
            return;
        }
    }

    void OnGUI()
    {
        if (!player || !Selected)
        {
            return;
        }
        var pos = CamMain.WorldToScreenPoint(Selected.transform.position);
        var inputs = player.Inputs.actions;

        GUI.Label(new Rect(pos.x-1.5f, Screen.height-pos.y, Screen.width, Screen.height), "<size=20><color=black><<Selected>></color></size>");
        GUI.Label(new Rect(pos.x, Screen.height-pos.y, Screen.width, Screen.height), "<size=20><color=yellow><<Selected>></color></size>");

        var switchtext = $"{inputs["Controller/ControllerSwitchUp"].bindings[0].ToDisplayString()} and {inputs["Controller/ControllerSwitchDown"].bindings[0].ToDisplayString()} To Cycle Between objects";

        GUI.Label(new Rect(48.5f, Screen.height - 150, Screen.width, Screen.height), $"<size=25><color=black>Press {inputs["Controller/ControllerExit"].bindings[0].ToDisplayString()} to Exit</color></size>");
        GUI.Label(new Rect(50, Screen.height - 150, Screen.width, Screen.height), $"<size=25><color=white>Press {inputs["Controller/ControllerExit"].bindings[0].ToDisplayString()} to Exit</color></size>");

        GUI.Label(new Rect(48.5f, Screen.height - 125, Screen.width, Screen.height),$"<size=25><color=black>{switchtext}</color></size>");
        GUI.Label(new Rect(50, Screen.height - 125, Screen.width, Screen.height), $"<size=25><color=white>{switchtext}</color></size>");

        var usetext = "";

        if(Selected is MovingPlataform)
        {
            var plat = Selected.GetComponent<MovingPlataform>();
            if(plat.directional == MovingPlataform.Directional.TopBottom || plat.directional == MovingPlataform.Directional.BottomTop)
            {
                usetext = $"Press {inputs["Controller/ControllerUp"].bindings[0].ToDisplayString()} To Move the plataform UP\n" +
                          $"Press {inputs["Controller/ControllerDown"].bindings[0].ToDisplayString()}To Move the plataform DOWN";
            }
            if (plat.directional == MovingPlataform.Directional.LeftRight || plat.directional == MovingPlataform.Directional.RightLeft)
            {
                usetext = $"Press {inputs["Controller/ControllerRight"].bindings[0].ToDisplayString()} To Move the plataform RIGHT\n" +
                          $"Press {inputs["Controller/ControllerLeft"].bindings[0].ToDisplayString()}To Move the plataform LEFT";
            }
        }

        GUI.Label(new Rect(48.5f, Screen.height - 100, Screen.width, Screen.height), $"<size=25><color=black>{usetext}</color></size>");
        GUI.Label(new Rect(50f, Screen.height - 100, Screen.width, Screen.height), $"<size=25><color=white>{usetext}</color></size>");
        //if (Selected is HeatObject)
        //{
        //    GUI.Label(new Rect(48.5f, Screen.height - 100, Screen.width, Screen.height), $"<size=25><color=black>{player.IM.MoveR} To Turn Heater/Cooler On/Off</color></size>");
        //    GUI.Label(new Rect(50, Screen.height - 100, Screen.width, Screen.height), $"<size=25><color=white>{player.IM.MoveR} To Turn Heater/Cooler On/Off</color></size>");
        //    return;
        //}
        //if (Selected is Laser)
        //{
        //    GUI.Label(new Rect(48.5f, Screen.height - 100, Screen.width, Screen.height), $"<size=25><color=black>{player.IM.MoveR} To Turn the Laser On/Off</color></size>");
        //    GUI.Label(new Rect(50, Screen.height - 100, Screen.width, Screen.height), $"<size=25><color=white>{player.IM.MoveR} To Turn the Laser On/Off</color></size>");
        //}
    }

    public void ToggleControl()
    {
        if (player)
        {
            tracking.target = player.transform;
            player.controller = null;
            player.Inputs.SwitchCurrentActionMap("Player");
            player = null;
            return;
        }
        player = Player.Instance;
        player.controller = this;
        tracking.target = CamPosition;
    }

    public void ControllerSwitchUp()
    {
        if (Props.Count == 0) return;
        if (Props.IndexOf(Selected) < Props.Count - 1)
        {
            Selected = Props[Props.IndexOf(Selected) + 1];
        }
    }

    public void ControllerSwitchDown()
    {
        if (Props.Count == 0) return;
        if (Props.IndexOf(Selected) > 0)
        {
            Selected = Props[Props.IndexOf(Selected) - 1];
        }
    }

    public void ControllerUp(InputValue value)
    {
        if (!Selected) return;
        if(Selected is MovingPlataform)
        {
            var  plat = Selected.GetComponent<MovingPlataform>();
            switch (plat.directional)
            {
                case MovingPlataform.Directional.BottomTop:
                    plat.Move = value.isPressed;
                    if (!value.isPressed) return;
                    plat.Open();
                    break;
                case MovingPlataform.Directional.TopBottom:
                    plat.Move = value.isPressed;
                    if (!value.isPressed) return;
                    plat.Close();
                    break;
                default:
                    break;
            }
        }
    }

    public void ControllerDown(InputValue value)
    {
        if (!Selected) return;
        if (Selected is MovingPlataform)
        {
            var plat = Selected.GetComponent<MovingPlataform>();
            switch (plat.directional)
            {
                case MovingPlataform.Directional.BottomTop:
                    plat.Move = value.isPressed;
                    if (!value.isPressed) return;
                    plat.Close();
                    break;
                case MovingPlataform.Directional.TopBottom:
                    plat.Move = value.isPressed;
                    if (!value.isPressed) return;
                    plat.Open();
                    break;
                default:
                    break;
            }
        }
    }

    public void ControllerLeft(InputValue value)
    {
        if (!Selected) return;
        if (Selected is MovingPlataform)
        {
            var plat = Selected.GetComponent<MovingPlataform>();
            switch (plat.directional)
            {
                case MovingPlataform.Directional.LeftRight:
                    plat.Move = value.isPressed;
                    //if (!value.isPressed) return;
                    plat.Close();
                    break;
                case MovingPlataform.Directional.RightLeft:
                    plat.Move = value.isPressed;
                    if(!value.isPressed) return;
                    plat.Open();
                    break;
                default:
                    break;
            }
        }
    }

    public void ControllerRight(InputValue value)
    {
        if (!Selected) return;
        if (Selected is MovingPlataform)
        {
            var plat = Selected.GetComponent<MovingPlataform>();
            switch (plat.directional)
            {
                case MovingPlataform.Directional.LeftRight:
                    plat.Move = value.isPressed;
                    if (!value.isPressed) return;
                    plat.Open();
                    break;
                case MovingPlataform.Directional.RightLeft:
                    plat.Move = value.isPressed;
                    if (!value.isPressed) return;
                    plat.Close();
                    break;
            }
        }
    }
}