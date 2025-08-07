using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private string DEFAULT_LEVEL_SCENE = "Level1";

    public void StartGame()
    {
        SceneManager.LoadScene(DEFAULT_LEVEL_SCENE);
    }
}
