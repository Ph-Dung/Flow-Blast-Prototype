using UnityEngine;

public class BoxClick : MonoBehaviour
{
    public Box box;
    private SlotManager slotManager;

    void Awake()
    {
        slotManager = FindFirstObjectByType<SlotManager>();
        box = GetComponent<Box>();
    }

    void OnMouseDown()
    {
        if (slotManager == null)
        {
            Debug.LogError("No SlotManager in scene");
            return;
        }

        slotManager.TryAddBox(box);
    }
}