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
        slotManager.TryAddBox(box);
    }
}