using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            // Preserve GameManager
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Prevent Duplicates
            Destroy(gameObject);
        }
    }
}
