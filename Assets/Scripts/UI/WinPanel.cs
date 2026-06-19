using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : MonoBehaviour
{
    public void Home()
    {
        SoundManager.Instance.PlayClick();
        SceneManager.LoadScene("MainMenu");
    }
    public void NextLevel()
    {
        SoundManager.Instance.PlayClick();
        SceneManager.LoadScene("Level 2");
    }
}
