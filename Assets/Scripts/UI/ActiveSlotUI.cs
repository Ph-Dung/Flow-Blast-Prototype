using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActiveSlotUI : MonoBehaviour
{
    public TMP_Text progressText;

    public void UpdateUI(Box box)
    {
        if (box == null)
        {
            progressText.text = "Empty";
            return;
        }
        progressText.text =
            $"{box.current}/{box.target}";
    }
}