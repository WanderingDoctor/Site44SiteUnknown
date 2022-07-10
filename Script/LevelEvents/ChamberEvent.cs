using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class ChamberEvent : MonoBehaviour
{
    #region variables
    //Built-in Listener Handler
    [HideInInspector]public UnityEvent Actions;

    //Index of the Event
    public int Index;
    #endregion

    #region statics
    //Invokes All Listeners in Events with given Index
    public static void InvokeEvents(int index) => FindObjectsOfType<ChamberEvent>().Where(e=>e.Index == index).ToList().ForEach(i=>i.Actions.Invoke());

    //Invokes All Listeners in Event with given GameobjectName
    public static void InvokeEvents(string name) => FindObjectsOfType<ChamberEvent>().Where(e => e.name == name).ToList().ForEach(i => i.Actions.Invoke());

    //Invokes All Listeners in All Events...Honestly just for the memes
    public static void InvokeALL()=> FindObjectsOfType<ChamberEvent>().ToList().ForEach(i => i.Actions.Invoke());
    #endregion
}