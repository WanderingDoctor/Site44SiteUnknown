using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SiteScript<T> : MonoBehaviour where T : SiteScript<T>
{
    public static T Instance
    {
        get
        {
            var _inst = FindObjectOfType(typeof(T),true) as T;
            if (!_inst)
            {
                Debug.LogError($"No Instance found for {typeof(T)}");
                return null;
            }
            return _inst;
        }
    }

    public static List<T> AllInstances
    {
        get
        {
            var _inst = FindObjectsOfType(typeof(T), true) as List<T>;
            if (_inst.Count < 0)
            {
                Debug.LogError($"No Instances found for {typeof(T)}");
                return null;
            }
            return _inst;
        }
    }
}
