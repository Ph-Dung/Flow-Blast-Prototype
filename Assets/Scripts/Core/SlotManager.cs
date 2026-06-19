using UnityEngine;
using System.Collections.Generic;

public class SlotManager : MonoBehaviour
{
    public List<ActiveSlot> slots = new List<ActiveSlot>();
    public Transform[] slotPositions;
    private int maxSlots = 3;
    public int activeSlot = 0;

    void Awake()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(new ActiveSlot());
        }
    }

    public bool IsBoxActive(Box box)
    {
        foreach (var slot in slots)
        {
            if (slot.box == box) return true;
        }
        return false;
    }

    public bool TryAddBox(Box box)
    {
        if (IsBoxActive(box)) return false;

        for (int i = 0; i < slots.Count; i++)
        {
            var slot = slots[i];
            if (slot.IsEmpty)
            {
                slot.box = box;
                box.MoveTo(slotPositions[i].position);
                activeSlot++;
                SoundManager.Instance.PlayClick();
                return true;
            }
        }
        Debug.Log("No empty slot!");
        return false;
    }

    public void RemoveBox(Box box)
    {
        foreach (var slot in slots)
        {
            if (slot.box == box)
            {
                slot.Clear();
                activeSlot--;
                if (box != null)
                {
                    Destroy(box.gameObject);
                }
                return;
            }
        }
    }

    public bool ReviveOneSlot()
    {
        for (int i = slots.Count - 1; i >= 0; i--)
        {
            var slot = slots[i];
            if (!slot.IsEmpty && slot.box != null)
            {
                Box box = slot.box;
                slot.Clear();
                activeSlot--;
                box.ReturnToBoard();
                return true;
            }
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        if (slotPositions == null) return;

        Gizmos.color = Color.green;

        foreach (var slot in slotPositions)
        {
            if (slot != null)
            {
                Gizmos.DrawSphere(slot.position, 0.2f);
            }
        }
    }
}
