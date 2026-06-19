using UnityEngine;

public class ExitZone : MonoBehaviour
{
    public SlotManager slotManager;
    public LevelManager levelManager;
    private float lastSuckTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Pixel pixel = other.GetComponent<Pixel>();
        if (pixel == null) return;

        foreach (var slot in slotManager.slots)
        {
            if (slot.box == null) continue;

            if (IsMatch(slot.box.color, pixel.color) && slot.box.current + slot.box.pixelsInFlight < slot.box.target)
            {
                if (levelManager != null && levelManager.trackManager != null)
                {
                    levelManager.trackManager.RemovePixel(pixel);
                }

                Box targetBox = slot.box;
                targetBox.pixelsInFlight++;

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

                if(Time.time - lastSuckTime > 0.05f)
                {
                    SoundManager.Instance.PlaySuck();
                    lastSuckTime = Time.time;
                }

                return;
            }
        }
    }

    private bool IsMatch(PixelColor boxColor, PixelColor pixelColor)
    {
        return boxColor == pixelColor;
    }
}