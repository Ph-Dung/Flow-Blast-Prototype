using UnityEngine;

public class Pixel : MonoBehaviour
{
    public PixelColor color;
    public float speed = 5f;

    private Transform[] path;
    private int index;

    private SpriteRenderer sr;
    private Vector3 target;

    private Transform suckTarget;
    private System.Action onSuckComplete;
    private bool isSucking = false;
    public float suckSpeed = 15f;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Init(Transform[] pathPoints, PixelColor c)
    {
        path = pathPoints;
        color = c;
        isSucking = false;
        suckTarget = null;
        onSuckComplete = null;

        if (TryGetComponent<Collider2D>(out var col))
        {
            col.enabled = true;
        }

        ApplyColor();

        if (path != null && path.Length > 0)
        {
            SetTarget(path[0].position);
            transform.position = path[0].position;
        }
    }

    void ApplyColor()
    {
        if (sr == null) return;

        sr.color = GetUnityColor(color);
    }

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

    public void SetTarget(Vector3 pos)
    {
        target = pos;
    }

    public void StartSucking(Transform targetBox, System.Action onComplete)
    {
        isSucking = true;
        suckTarget = targetBox;
        onSuckComplete = onComplete;

        if (TryGetComponent<Collider2D>(out var col))
        {
            col.enabled = false;
        }
    }

    void Update()
    {
        if (isSucking)
        {
            if (suckTarget == null)
            {
                Destroy(gameObject);
                return;
            }

            transform.position = Vector3.MoveTowards(
                transform.position,
                suckTarget.position,
                suckSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, suckTarget.position) < 0.1f)
            {
                onSuckComplete?.Invoke();
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                speed * Time.deltaTime
            );
        }
    }
}