using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public List<GameObject> Components;

    public TMP_Text scene;

    private void OnEnable()
    {
        scene.text = SceneManager.GetActiveScene().name;
        Player.Instance.Inputs.SwitchCurrentActionMap("UI");
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Player.Instance.Inputs.SwitchCurrentActionMap("Player");
        Components.ForEach(c=>c.SetActive(false));
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        var active = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(active, LoadSceneMode.Single);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu",LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
