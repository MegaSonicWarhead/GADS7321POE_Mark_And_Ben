using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public string gameSceneName = "Game"; // Set this in the Inspector

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}
