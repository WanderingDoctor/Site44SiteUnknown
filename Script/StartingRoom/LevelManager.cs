using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Diagnostics;

public class LevelManager : SiteScript<LevelManager>
{
    public GameObject PauseMenu;

    Player player;

    Timer timer;

    public List<Transform> Checkpoints;
    public Transform CurCheckpoint;

    public List<float> Times;

    void Start()
    {
        timer = Timer.Instance;
        player = Player.Instance;
        CurCheckpoint = Checkpoints[0];
    }

    void Update()
    {

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            PauseMenu.SetActive(!PauseMenu.activeSelf);
            if (timer.timer != null && timer.timer.IsRunning)
            {
                timer.TimerPause();
                return;
            }
            timer.TimerResume();
            return;
        }
    }

    public void StartLevel()
    {
        timer.TimerStart();
    }

    public void FinishLevel()
    {
        timer.TimerEnd();
        FinalCheckpoint();
    }

    public void FinalCheckpoint()
    {
        if (Checkpoints.Count > 0 && Checkpoints.IndexOf(CurCheckpoint) + 1 != Checkpoints.Count)
        {
            CurCheckpoint = Checkpoints[Checkpoints.IndexOf(CurCheckpoint) + 1];
            player.SpawnPoint = CurCheckpoint;
        }
    }

    public void NextCheckpoint()
    {
        if (Checkpoints.Count > 0 && Checkpoints.IndexOf(CurCheckpoint) + 1 != Checkpoints.Count)
        {
            timer.TimerCheckpoint();
            CurCheckpoint = Checkpoints[Checkpoints.IndexOf(CurCheckpoint) + 1];
            player.SpawnPoint = CurCheckpoint;
        }
    }
}
