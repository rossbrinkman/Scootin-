using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Hosting;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static bool GameIsOver = false;
    public GameObject gameOverUI;

    void Update()
    {
        if (GameIsOver)
        {
            gameOver();
        }

    }


    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu Test Scene");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Downhill");
        GameIsOver = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void gameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsOver = true;
    }
}
