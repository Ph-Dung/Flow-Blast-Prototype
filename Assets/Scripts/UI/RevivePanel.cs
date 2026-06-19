using UnityEngine;

/// <summary>
/// Panel hiển thị khi người chơi thua.
/// - Bấm Revive  → hồi sinh và chơi tiếp
/// - Bấm X/Skip  → đóng panel này và hiện LosePanel
/// </summary>
public class RevivePanel : MonoBehaviour
{
    public void OnRevive()
    {
        SoundManager.Instance.PlayClick();
        GameManager.Instance.Revive();
    }

    public void OnSkip()
    {
        SoundManager.Instance.PlayClick();
        GameManager.Instance.ShowLosePanel();
    }
}
