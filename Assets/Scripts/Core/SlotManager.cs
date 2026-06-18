using UnityEngine;
using System.Collections.Generic;

public class SlotManager : MonoBehaviour
{
    public List<ActiveSlot> slots = new List<ActiveSlot>();
    public Transform[] slotPositions;

    void Awake()
    {
        for (int i = 0; i < 3; i++)
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
                Debug.Log($"Box {box.color} assigned to slot {i}");

                if (slotPositions != null && i < slotPositions.Length && slotPositions[i] != null)
                {
                    box.MoveTo(slotPositions[i].position);
                }
                else
                {
                    // Fallback relative to SlotManager transform position
                    Vector3 fallbackPos = transform.position + new Vector3((i - 1) * 1.5f, 0, 0);
                    box.MoveTo(fallbackPos);
                }

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
                if (box != null)
                {
                    Destroy(box.gameObject);
                }
                return;
            }
        }
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
