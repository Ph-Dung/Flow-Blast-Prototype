using UnityEngine;

public class CollectZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Pixel pixel = other.GetComponent<Pixel>();

        if (pixel == null)
            return;

        Debug.Log($"Pixel Enter: {pixel.pixelColor}");

        Destroy(pixel.gameObject);
    }
}