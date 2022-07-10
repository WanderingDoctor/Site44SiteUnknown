using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public TMP_Text version;

    private void Start()
    {
        version.text = Application.version;
        OptionsMenu.Instance.Initialize();
        Timer.Instance.gameObject.SetActive(false);
    }
    public void LoadLevel(string level)=>SceneManager.LoadScene(level);
    public void QuitGame() 
    {
        Application.Quit();
    }
}