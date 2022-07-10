using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    TMP_Text Fps;

    private void Start()
    {
        Fps = GetComponent<TMP_Text>();
    }

    void Update()
    {
        FPSCheck();
    }

    void FPSCheck()
    {
        if (Time.timeScale < 1)
        {
            Fps.text = $"{(int)(1 / Time.unscaledDeltaTime)}";
            return;
        }
        Fps.text = $"{(int)(1 / Time.smoothDeltaTime)}";
    }
}
