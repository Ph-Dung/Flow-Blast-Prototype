using UnityEngine;

public enum GameState
{
    Playing,
    Won,
    Lost
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public LevelManager levelManager;

    [Header("UI Panels")]
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject revivePanel;
    public UIManager uiManager;

    private bool hasRevived = false;
    public GameState CurrentState { get; private set; } = GameState.Playing;

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

    void Update()
    {
        if (CurrentState == GameState.Playing)
        {
            CheckGameStatus();
        }
    }

    public void StartGame()
    {
        Debug.Log("Game Start");
        CurrentState = GameState.Playing;
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        if (revivePanel != null) revivePanel.SetActive(false);
        levelManager.InitLevel();
    }

    public void RestartLevel()
    {
        CurrentState = GameState.Playing;
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        revivePanel.SetActive(false);
        levelManager.InitLevel();
    }

    public void WinGame()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.Won;
        uiManager.ShowGameplayUI(false);
        winPanel.SetActive(true);
        SoundManager.Instance.PlayWin();
    }

    public void LoseGame()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.Lost;
        uiManager.ShowGameplayUI(false);
        if (!hasRevived)
            revivePanel.SetActive(true);
        else
            ShowLosePanel();
    }

    public void ShowLosePanel()
    {
        revivePanel.SetActive(false);
        losePanel.SetActive(true);
        SoundManager.Instance.PlayLose();
    }

    public void Revive()
    {
        if (CurrentState != GameState.Lost) return;

        if (levelManager != null && levelManager.slotManager != null)
        {
            bool revived = levelManager.slotManager.ReviveOneSlot();
            if (revived)
            {
                CurrentState = GameState.Playing;
                hasRevived = true;
                uiManager.ShowGameplayUI(true);
                if (revivePanel != null) revivePanel.SetActive(false);
                if (losePanel != null) losePanel.SetActive(false);
            }
        }
    }

    private void CheckGameStatus()
    {
        if (levelManager == null) return;

        // 1. Check Win Condition
        // Win when all boxes are completed
        if (levelManager.remainingBoxes <= 0)
        {
            WinGame();
            return;
        }

        // 2. Check Lose Condition
        // "thua nếu active slot full, các pixel trên vòng quay đã đầy mà không có pixel nào phù hợp để các active slot có thể hút"
        if (levelManager.slotManager != null && levelManager.trackManager != null)
        {
            var slotManager = levelManager.slotManager;
            var trackManager = levelManager.trackManager;

            bool isSlotsFull = slotManager.activeSlot >= slotManager.slots.Count;
            bool isTrackFull = trackManager.activePixels.Count >= trackManager.capacity;
            bool noMoreSpawnsPossible = !levelManager.HasPixel() && trackManager.waitingPixels.Count == 0;

            // Lose if slots are full AND (track is full OR no new pixels can spawn)
            if (isSlotsFull && (isTrackFull || noMoreSpawnsPossible))
            {
                // Check if any active pixel on track can be sucked by any active slot box
                bool hasMatch = false;
                foreach (var pixel in trackManager.activePixels)
                {
                    if (pixel == null) continue;
                    foreach (var slot in slotManager.slots)
                    {
                        if (slot.box != null)
                        {
                            if (slot.box.color == pixel.color && slot.box.current + slot.box.pixelsInFlight < slot.box.target)
                            {
                                hasMatch = true;
                                break;
                            }
                        }
                    }
                    if (hasMatch) break;
                }

                if (!hasMatch)
                {
                    LoseGame();
                }
            }
        }
    }
}