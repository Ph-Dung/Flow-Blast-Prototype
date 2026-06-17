using UnityEngine;

public class Box : MonoBehaviour
{
    public PixelColor BoxColor;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        GameManager.Instance.SelectBox(this);
    }
}