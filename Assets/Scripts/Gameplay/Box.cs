using UnityEngine;

public class Box : MonoBehaviour
{
    public PixelColor color;
    public int target = 5;
    public int current = 0;
    [HideInInspector]
    public int pixelsInFlight = 0;

    private LevelManager levelManager;

    private Vector3 targetPosition;
    private bool hasTargetPosition = false;
    public float moveSpeed = 10f;

    void Start()
    {
        GetComponent<SpriteRenderer>().color = GetUnityColor(color);

        levelManager = FindFirstObjectByType<LevelManager>();
        targetPosition = transform.position;
    }

    public void MoveTo(Vector3 pos)
    {
        targetPosition = pos;
        hasTargetPosition = true;
    }

    void Update()
    {
        if (hasTargetPosition)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
        }
    }

    public void AddPixel()
    {
        current++;

        if (IsComplete)
        {
            levelManager.OnBoxCompleted(this);
        }
    }

    public bool IsComplete => current >= target;

    private Color GetUnityColor(PixelColor color)
    {
        switch (color)
        {
            case PixelColor.Red: return Color.red;
            case PixelColor.Blue: return Color.blue;
            case PixelColor.Green: return Color.green;
            case PixelColor.Yellow: return Color.yellow;
            case PixelColor.Purple: return Color.purple;
            case PixelColor.Orange: return Color.orange;
            case PixelColor.Cyan: return Color.cyan;
            case PixelColor.Pink: return Color.pink;
            default: return Color.white;
        }
    }
}