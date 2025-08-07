using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject pauseMenu;
    private string MAIN_MENU_SCENE = "MainMenu";
    private bool isPaused;

    void Start()
    {
        isPaused = false;
        pauseMenu.SetActive(false);   
    }

    void Update()
    {
        TogglePause();
    }

    public void TogglePause()
    {
        if (SceneManager.GetActiveScene().name != MAIN_MENU_SCENE)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPaused)
                {
                    Time.timeScale = 0f;
                    pauseMenu.SetActive(true);
                    isPaused = true;
                }
                else
                {
                    Time.timeScale = 1f;
                    pauseMenu.SetActive(false);
                    isPaused = false;
                }
            }
        }
    }

    public void ClickMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MAIN_MENU_SCENE);
    }

    public void ClickResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        isPaused = false;
    }
}
