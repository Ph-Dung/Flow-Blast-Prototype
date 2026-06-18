using UnityEngine;

[System.Serializable]
public class BoxData
{
    public PixelColor color;
    public int amount;
}

[CreateAssetMenu(menuName = "Game/Level")]
public class LevelData : ScriptableObject
{
    public BoxData[] boxes;
    public int trackCapacity = 10;
}