using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropInstantiator : ChamberProp
{
    public int InitialUses;

    public int Uses;

    public bool Unlimited;

    public GameObject Spawn;

    public Transform SpawnPosition;

    void Update()
    {
        if (Unlimited)
        {
            Uses = 999;
            return;
        }
    }

    public override void Initialize()
    {
        Uses = InitialUses;
    }

    public override void reset()
    {
        Uses = InitialUses;
    }

    public void SpawnProp()
    {
        if(Uses > 0)
        {
            Instantiate(Spawn,SpawnPosition.position,Quaternion.identity);
            Uses -= 1;
        }
    }
}
