using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPasued = false;
    public GameObject pauseMeueUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)) //should use vr input
        {
            Debug.Log("Space key is pressed.");
            if (GameIsPasued)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
    }

    public void Resume()
    {
        pauseMeueUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPasued = false;
    }
    void Pause()
    {
        pauseMeueUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPasued = true;

    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quiting menu...");
        Application.Quit();
    }

    public void Practice()
    {
        Debug.Log("Loading Practice...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("PracticeScene");
    }

    public void LoadGame()
    {
        Debug.Log("Loading Game...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("FirePlayScene");
    }
}
