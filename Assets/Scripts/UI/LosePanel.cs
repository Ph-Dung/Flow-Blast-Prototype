using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    public void Home()
    {
        SoundManager.Instance.PlayClick();
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartLevel()
    {
        SoundManager.Instance.PlayClick();
        GameManager.Instance.RestartLevel();
    }
}
