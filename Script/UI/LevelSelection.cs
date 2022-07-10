using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelSelection : SiteScript<LevelSelection>
{
    public ScrollRect ActHandler;

    public List<GameObject> Acts;

    private void Start()
    {
        SelectChapter(0);
    }
    public void SelectChapter(int selected)
    {
        Acts[selected].SetActive(true);
        ActHandler.content = Acts[selected].GetComponent<RectTransform>();
        Acts.ForEach(act =>
        {
            if(Acts.IndexOf(act) != selected)
            {
                act.SetActive(false);
            }
        });
    }
    public void SelectLevel(Level level)
    {
        SceneManager.LoadScene(level.LevelScene);
    }
}
[Serializable]public struct Level
{
    public string LevelScene;
    public string LevelName;
    public Sprite LevelPreview;
}