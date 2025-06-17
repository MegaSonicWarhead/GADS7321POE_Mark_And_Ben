using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;
    public GameObject PauseBTN;
    public GameObject MemoryMenu;
    void Start()
    {
        pauseMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void GamePausing()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }
    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public GameObject SettingMenu;

    public void SettShown()
    {
        SettingMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void SettHidden()
    {
        SettingMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void PuzzleShown()
    {
        MemoryMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void PuzzleHidden()
    {
        MemoryMenu.SetActive( false);
        pauseMenu.SetActive(true);
    }
}