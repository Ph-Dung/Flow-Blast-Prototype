using UnityEngine;

public class ExitZone : MonoBehaviour
{
    public SlotManager slotManager;
    public LevelManager levelManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Pixel pixel = other.GetComponent<Pixel>();
        if (pixel == null) return;

        foreach (var slot in slotManager.slots)
        {
            if (slot.box == null) continue;

            // Check if color matches and the box still needs more pixels (including ones in flight)
            if (IsMatch(slot.box.color, pixel.color) && slot.box.current + slot.box.pixelsInFlight < slot.box.target)
            {
                // Remove pixel from TrackManager immediately so it's not positioned/updated by track rotation
                if (levelManager != null && levelManager.trackManager != null)
                {
                    levelManager.trackManager.RemovePixel(pixel);
                }

                Box targetBox = slot.box;
                targetBox.pixelsInFlight++;

                // Start sucking animation
                pixel.StartSucking(targetBox.transform, () =>
                {
                    if (targetBox != null)
                    {
                        targetBox.pixelsInFlight = Mathf.Max(0, targetBox.pixelsInFlight - 1);
                        targetBox.AddPixel();
                        
                        if (levelManager != null)
                        {
                            levelManager.OnPixelConsumed();
                        }

                        if (targetBox.IsComplete)
                        {
                            slotManager.RemoveBox(targetBox);
                        }
                    }
                });

                return;
            }
        }
    }

    bool IsMatch(PixelColor boxColor, PixelColor pixelColor)
    {
        return boxColor == pixelColor;
    }
}