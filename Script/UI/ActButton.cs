using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActButton : MonoBehaviour
{
    public int act;
    private void Start()
    {
        var select = LevelSelection.Instance;
        GetComponent<Button>().onClick.AddListener(()=>select.SelectChapter(act));
        transform.GetChild(0).GetComponent<TMP_Text>().text = $"Act {act}";
    }
}