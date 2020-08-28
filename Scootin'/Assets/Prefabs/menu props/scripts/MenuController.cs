using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Camera_Shifter script;
    public void start()
    {
        script.play();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Settings()
    {
        script.settings();
    }
    public void Credits()
    {
        script.credits();
    }
    public void Achievements()
    {
        script.achievements();
    }
    public void Shop()
    {
        script.shop();
    }
    public void Replay()
    {
        script.replay();
    }
    public void antiSettings()
    {
        script.antisettings();
    }
    public void antiCredits()
    {
        script.anticredits();
    }
    public void antiAchievements()
    {
        script.antiachievements();
    }
    public void antiShop()
    {
        script.antishop();
    }
    public void antiReplay()
    {
        script.antireplay();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
