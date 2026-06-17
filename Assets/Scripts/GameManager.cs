using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Box SelectedBox;

    private void Awake()
    {
        Instance = this;
    }

    public void SelectBox(Box box)
    {
        SelectedBox = box;
        Debug.Log($"Selected: {box.BoxColor}");
    }
}
