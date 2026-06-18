using UnityEngine;

public class PixelSpawner : MonoBehaviour
{
    public GameObject pixelPrefab;
    public TrackManager track;
    public Pixel Spawn(PixelColor color)
    {
        GameObject obj = Instantiate(pixelPrefab, transform.position, Quaternion.identity);

        Pixel pixel = obj.GetComponent<Pixel>();
        pixel.Init(track.trackPoints, color);

        return pixel;
    }
}