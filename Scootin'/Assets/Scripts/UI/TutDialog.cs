using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


//This script holds all the tutorial windows and sets the conditions and actions when the tutorials are opened. -G$
public class TutDialog : MonoBehaviour
{
    public bool tutOn = false;

    public GameObject displayBox;
    public GameObject displayBox1;
    public GameObject winBox;
    //public Animator animator;
    public TMP_Text popUpText;
    public TMP_Text popUp1Text;
    public TMP_Text popUp2Text;
    public TMP_Text popUp3Text;
    public TMP_Text tricksText;
    public TMP_Text grindText;
    public TMP_Text winText;

    public GameObject lineController;


    public void PopUp (string text)
    {
        lineController.SetActive(false);
        displayBox.SetActive(true);
        popUpText.text = text;
        tutOn = true;
        Time.timeScale = 0f;
    }

    public void PopUp1(string text)
    {
        lineController.SetActive(false);
        displayBox1.SetActive(true);
        popUp1Text.text = text;
        tutOn = true;
        Time.timeScale = 0f;
    }

    public void PopUp2(string text)
    {
        lineController.SetActive(false);
        displayBox1.SetActive(true);
        popUp2Text.text = text;
        tutOn = true;
        Time.timeScale = 0f;
    }

    public void PopUp3(string text)
    {
        lineController.SetActive(false);
        displayBox1.SetActive(true);
        popUp3Text.text = text;
        tutOn = true;
        Time.timeScale = 0f;
    }
    public void TricksText(string text)
    {
        lineController.SetActive(false);
        displayBox1.SetActive(true);
        tricksText.text = text;
        tutOn = true;
        Time.timeScale = 0f;
    }
    public void GrindText(string text)
    {
        lineController.SetActive(false);
        displayBox1.SetActive(true);
        grindText.text = text;
        tutOn = true;
        Time.timeScale = 0f;
    }

    public void WinText(string text)
    {
        lineController.SetActive(false);
        winBox.SetActive(true);
        winText.text = text;
        tutOn = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        lineController.SetActive(true);
        displayBox.SetActive(false);
        displayBox1.SetActive(false);
        Time.timeScale = 1f;
        tutOn = false;
    }

    public void Finish()
    {
        Time.timeScale = 1f;
        tutOn = false;
        winBox.SetActive(false);
        SceneManager.LoadScene("Menu Test Scene");
    }
}
