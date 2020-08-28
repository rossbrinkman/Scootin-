using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Hosting;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    public GameObject lineController;

    //void Start()
    //{
    //    drawLine = GameObject.FindGameObjectWithTag("Player").GetComponent<DrawLine>();
    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } 
            
            else
            {
                Pause();
            }
        }
    }

    public void Resume ()
    {
        lineController.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void LoadMenu ()
    {
        //Debug.Log("Loading Menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu Test Scene");
    }

    public void QuitGame ()
    {
        //Debug.Log("Quitting Game...");
        Application.Quit();
    }

    void Pause ()
    {
        lineController.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
