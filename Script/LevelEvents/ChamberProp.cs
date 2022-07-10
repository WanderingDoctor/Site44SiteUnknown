using System.Collections;
using System.Reflection;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public abstract class ChamberProp : MonoBehaviour
{
    [Serializable]public struct Triggers
    {        
        public ChamberEvent CEvent; 
        public List<string> Methods;
    }public List<Triggers> triggers; 

    public string[] ToExecute;

    private void OnValidate()
    {
        ToExecute = GetAllMethods();
    }

    void Awake(){
        triggers.ForEach(t=>t.Methods.ForEach((m)=>{
            Debug.Log($"Adding {m} to {t.CEvent}");
            t.CEvent.Actions.AddListener(()=>Invoke(m,0));
        }));
    }

    void Start()=>Initialize();
    public abstract void Initialize();
    public abstract void reset();
#if UNITY_EDITOR
    internal string[] GetAllMethods()
    {
        List<string> found = new List<string>();
        GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).ToList().ForEach(m=>found.Add(m.Name));
        return found.ToArray();
    }
#endif
}