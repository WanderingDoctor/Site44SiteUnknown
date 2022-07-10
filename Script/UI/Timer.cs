using System.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : SiteScript<Timer>
{
    public TMP_Text CheckpointTimes,CurrentTime;
    public Stopwatch timer;
    List<string> Checkpoints = new List<string>();
    void Start()
    {
        CurrentTime = GetComponent<TMP_Text>();
        CheckpointTimes = transform.GetChild(0).GetComponent<TMP_Text>();
        CurrentTime.text = "";
    }

    void Update()
    {
        CheckpointTimes.text = "";
        Checkpoints.ForEach((cp) =>
        {
            CheckpointTimes.text += $"{cp}\n";
        });
        if (timer != null && timer.IsRunning)
        {
            CurrentTime.text = $"{timer.Elapsed.Minutes}:{timer.Elapsed.Seconds}:{timer.Elapsed.Milliseconds}";
        }
    }

    public void TimerStart()
    {
        TimerReset();
        timer.Start();
    }
    public void TimerResume() => timer?.Start();
    public void TimerCheckpoint()=>Checkpoints.Add($"{timer.Elapsed.Minutes}:{timer.Elapsed.Seconds}:{timer.Elapsed.Milliseconds}");
    public void TimerPause()=>timer.Stop();

    public void TimerEnd()
    {
        TimerCheckpoint();
        CurrentTime.text = $"{timer.Elapsed.Minutes}:{timer.Elapsed.Seconds}:{timer.Elapsed.Milliseconds}";
        timer = null;
    }

    public void TimerReset()
    {
        timer = new Stopwatch();
        Checkpoints = new List<string>();
    }
}
