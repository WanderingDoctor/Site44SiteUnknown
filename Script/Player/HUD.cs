using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class HUD : SiteScript<HUD>
{
    public GUISkin guiskin;

    public GameObject SubtitleGameobject;

    public TextMeshProUGUI Subtitle;

    public List<string> Subtitles = new List<string>();

    public GameObject PrefabText;

    void Update()
    {
        SubtitleCheck();
    }

    void SubtitleCheck()
    {
        if (Subtitle.text == "")
        {
            SubtitleGameobject.SetActive(false);
            return;
        }
        SubtitleGameobject.SetActive(true);
    }
}