using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelData levelData;
    public TrackManager trackManager;
    public BoxSpawner boxSpawner;

    public SlotManager slotManager;

    private Queue<PixelColor> pixelQueue = new Queue<PixelColor>();

    public int remainingBoxes;

    public void InitLevel()
    {
        CleanupLevel();

        boxSpawner.SpawnBoxes(levelData);

        GeneratePixels(levelData);
        remainingBoxes = levelData.boxes.Length;
        FillTrack();
    }

    private void CleanupLevel()
    {
        // 1. Destroy all boxes
        Box[] existingBoxes = FindObjectsByType<Box>(FindObjectsSortMode.None);
        foreach (var b in existingBoxes)
        {
            if (b != null) Destroy(b.gameObject);
        }

        // 2. Destroy all pixels
        Pixel[] existingPixels = FindObjectsByType<Pixel>(FindObjectsSortMode.None);
        foreach (var p in existingPixels)
        {
            if (p != null) Destroy(p.gameObject);
        }

        // 3. Clear slot manager slots
        if (slotManager == null)
        {
            slotManager = FindFirstObjectByType<SlotManager>();
        }
        if (slotManager != null)
        {
            foreach (var slot in slotManager.slots)
            {
                slot.Clear();
            }
            slotManager.activeSlot = 0;
        }

        // 4. Clear track manager lists
        if (trackManager != null)
        {
            trackManager.activePixels.Clear();
            trackManager.waitingPixels.Clear();
        }
    }

    private void GeneratePixels(LevelData level)
    {
        pixelQueue.Clear();
        List<PixelColor> tempList = new List<PixelColor>();

        foreach (var box in level.boxes)
        {
            for (int i = 0; i < 5; i++)
            {
                tempList.Add(box.color);
            }
        }

        // Shuffle colors randomly using Fisher-Yates
        for (int i = tempList.Count - 1; i > 0; i--)
        {
            int r = Random.Range(0, i + 1);
            PixelColor temp = tempList[i];
            tempList[i] = tempList[r];
            tempList[r] = temp;
        }

        foreach (var color in tempList)
        {
            pixelQueue.Enqueue(color);
        }
    }

    public bool HasPixel()
    {
        return pixelQueue.Count > 0;
    }

    public PixelColor GetNextPixel()
    {
        return pixelQueue.Dequeue();
    }

    private void FillTrack()
    {
        if (trackManager != null)
        {
            trackManager.TrySpawn();
        }
    }

    public void OnPixelConsumed()
    {
        FillTrack();
    }
    public void OnBoxCompleted(Box box)
    {
        remainingBoxes--;
        if (remainingBoxes <= 0)
        {
            GameManager.Instance.WinGame();
        }
    }
}