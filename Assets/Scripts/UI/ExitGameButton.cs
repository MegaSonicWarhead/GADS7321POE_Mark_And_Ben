using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
