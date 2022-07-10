using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    public Level level;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(()=>LevelSelection.Instance.SelectLevel(level));
        transform.Find("LevelPreview").GetComponent<Image>().sprite = level.LevelPreview;
        transform.Find("LevelName").GetComponent<TMP_Text>().text = level.LevelName;
    }
}
