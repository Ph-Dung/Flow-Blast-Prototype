using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SoundManager.Instance.PlayClick();
        SceneManager.LoadScene("Level 1");
    }
    public void QuitGame()
    {
        SoundManager.Instance.PlayClick();
        Application.Quit();
    }
}
