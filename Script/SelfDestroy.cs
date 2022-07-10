using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public int delay;

    void Awake()=>SelfKill();

    public IEnumerator SelfKill()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
