using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLenth = 2f;

    private TutDialog tutDialog;
    private bool tutorialActive = false;

    private PauseMenu pauseMenu;

    public string TutorialScene;


    private void Awake()
    {
        Debug.Log("Awake:" + SceneManager.GetActiveScene().name);
    }

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == TutorialScene)
        {
            tutDialog = FindObjectOfType<TutDialog>();
            tutorialActive = true;
        }
                
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void Update()
    {
        if (tutorialActive)
        {
            if (tutDialog.tutOn == true)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale += (1f / slowdownLenth) * Time.unscaledDeltaTime;
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
                if (Time.timeScale == 1.0f)
                    Time.fixedDeltaTime = Time.deltaTime;
            }
        }

        if (pauseMenu.GameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale += (1f / slowdownLenth) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            if (Time.timeScale == 1.0f)
                Time.fixedDeltaTime = Time.deltaTime;
        }
    }

    public void DoSlowMotion()
    {
        //Time.timeScale = slowdownFactor;
        //Time.fixedDeltaTime = Time.timeScale * 0.5f;
    }
}