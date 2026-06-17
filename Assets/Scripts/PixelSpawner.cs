using UnityEngine;

public class PixelSpawner : MonoBehaviour
{
    [SerializeField] private Pixel pixelPrefab;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnPixel), 1f, 1f);
    }

    private void SpawnPixel()
    {
        Pixel pixel = Instantiate(
            pixelPrefab,
            transform.position,
            Quaternion.identity);

        PixelColor randomColor =
            (PixelColor)Random.Range(0, 5);

        pixel.Initialize(randomColor);
    }
}