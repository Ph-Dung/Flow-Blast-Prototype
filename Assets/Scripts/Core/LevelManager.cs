using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelData levelData;
    public TrackManager trackManager;
    public BoxSpawner boxSpawner;

    private Queue<PixelColor> pixelQueue = new Queue<PixelColor>();

    public int remainingBoxes;

    public void InitLevel()
    {
        boxSpawner.SpawnBoxes(levelData);

        GeneratePixels(levelData);

        FillTrack();
    }

    void GeneratePixels(LevelData level)
    {
        pixelQueue.Clear();
        List<PixelColor> tempList = new List<PixelColor>();

        foreach (var box in level.boxes)
        {
            for (int i = 0; i < box.amount; i++)
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

    void FillTrack()
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
            Debug.Log("LEVEL COMPLETE!");
        }
    }
}