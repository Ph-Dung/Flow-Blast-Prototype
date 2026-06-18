using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public LevelManager levelManager;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        Debug.Log("Game Start");
        levelManager.InitLevel();
    }

    public void RestartLevel()
    {
        Debug.Log("Restart Level");
        levelManager.InitLevel();
    }
}