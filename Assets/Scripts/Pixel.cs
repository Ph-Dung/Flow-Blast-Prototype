using UnityEngine;

public class Pixel : MonoBehaviour
{
    public PixelColor pixelColor;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(PixelColor color)
    {
        pixelColor = color;

        switch (color)
        {
            case PixelColor.Red:
                spriteRenderer.color = Color.red;
                break;

            case PixelColor.Blue:
                spriteRenderer.color = Color.blue;
                break;

            case PixelColor.Green:
                spriteRenderer.color = Color.green;
                break;
            case PixelColor.Yellow:
                spriteRenderer.color = Color.yellow;
                break;
            case PixelColor.Pink:
                spriteRenderer.color = Color.pink;
                break;
            case PixelColor.Purple:
                spriteRenderer.color = Color.purple;
                break;
        }
    }
}