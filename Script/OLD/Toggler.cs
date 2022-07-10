using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggler : MonoBehaviour
{
    public List<GameObject> TogglerTarget;
    public void Toggle()=>TogglerTarget.ForEach(t=>t.SetActive(!t.activeSelf));
}
