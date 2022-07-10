using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]public class ChamberTrigger : ChamberEvent
{
    //Properties of trigger
    public bool Checkpoint,Finish,start;

    public LevelManager LM;

    private void Awake()
    {
        //Debug.Log($"{this}:{LevelManager.Instance}");
        LM = LevelManager.Instance;
    }

    public void Activate(){
        if(!LM){
            Debug.LogWarning($"{this}:No Level Manager Found");
            return;
        }
        if(Checkpoint)LM.NextCheckpoint();
        if(Finish)LM.FinishLevel();
        if(start)LM.StartLevel();
        Actions.Invoke();
    }
}