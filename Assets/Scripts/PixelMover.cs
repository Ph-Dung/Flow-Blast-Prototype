using UnityEngine;

public class PixelMover : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}