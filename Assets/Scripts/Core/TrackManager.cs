using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public int capacity = 20;
    public List<Pixel> activePixels = new List<Pixel>();

    public Transform[] trackPoints;

    public bool HasSlot => activePixels.Count < capacity;
    public LevelManager levelManager;
    public GameObject pixelPrefab;

    public float rotationSpeed = 30f;
    private float currentAngleOffset = 0f;

    [Header("Waiting Queue Settings")]
    public int waitingQueueCapacity = 5;
    public Vector3 waitingQueueOffset = new Vector3(-3.5f, 0, 0);
    public Vector3 waitingQueueDirection = new Vector3(-0.6f, 0, 0);
    public List<Pixel> waitingPixels = new List<Pixel>();

    void Start()
    {
        Debug.Log("TRACK START");
        TrySpawn();
    }

    void Update()
    {
        TryMoveWaitingToActive();
        UpdateWaitingQueue();

        if (activePixels.Count > 0)
        {
            currentAngleOffset += rotationSpeed * Time.deltaTime;
            if (currentAngleOffset >= 360f) currentAngleOffset -= 360f;
        }
        UpdatePositions();
    }

    public void TrySpawn()
    {
        TryMoveWaitingToActive();
        UpdateWaitingQueue();
    }

    private void TryMoveWaitingToActive()
    {
        waitingPixels.RemoveAll(p => p == null);

        while (HasSlot && waitingPixels.Count > 0)
        {
            Pixel pixel = waitingPixels[0];
            waitingPixels.RemoveAt(0);

            activePixels.Add(pixel);
        }
    }

    private void UpdateWaitingQueue()
    {
        waitingPixels.RemoveAll(p => p == null);

        while (waitingPixels.Count < waitingQueueCapacity && levelManager.HasPixel())
        {
            PixelColor color = levelManager.GetNextPixel();

            Vector3 spawnPos = transform.position + waitingQueueOffset + waitingQueueDirection * waitingPixels.Count;
            GameObject obj = Instantiate(pixelPrefab, spawnPos, Quaternion.identity);

            Pixel pixel = obj.GetComponent<Pixel>();
            pixel.Init(trackPoints, color);

            waitingPixels.Add(pixel);
        }

        for (int i = 0; i < waitingPixels.Count; i++)
        {
            Vector3 targetPos = transform.position + waitingQueueOffset + waitingQueueDirection * i;
            waitingPixels[i].SetTarget(targetPos);
        }
    }

    public void AddPixel(Pixel pixel)
    {
        activePixels.Add(pixel);

        UpdatePositions();
    }

    public void RemovePixel(Pixel pixel)
    {
        activePixels.Remove(pixel);

        UpdatePositions();
        TrySpawn();
    }

    void UpdatePositions()
    {
        activePixels.RemoveAll(p => p == null);

        for (int i = 0; i < activePixels.Count; i++)
        {
            float angle = i * (360f / activePixels.Count) + currentAngleOffset;

            Vector3 pos = transform.position + GetCirclePosition(angle);

            activePixels[i].SetTarget(pos);
        }
    }

    Vector3 GetCirclePosition(float angle)
    {
        float radius = 1.5f;

        float rad = angle * Mathf.Deg2Rad;

        return new Vector3(
            Mathf.Cos(rad) * radius,
            Mathf.Sin(rad) * radius,
            0
        );
    }
}