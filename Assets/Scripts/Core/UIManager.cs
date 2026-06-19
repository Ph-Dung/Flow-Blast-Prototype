using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameplayUI;
    public SlotManager slotManager;
    public ActiveSlotUI[] slotUIs;
    public TMP_Text activeSlotText;
    void Update()
    {
        activeSlotText.text = $"Active Slots: {slotManager.activeSlot}/{slotManager.slots.Count}";
        for (int i = 0; i < slotUIs.Length; i++)
        {
            if (i < slotManager.slots.Count)
            {
                slotUIs[i].UpdateUI(
                    slotManager.slots[i].box
                );
            }
        }
    }
    public void ShowGameplayUI(bool show)
    {
        gameplayUI.SetActive(show);
    }
}