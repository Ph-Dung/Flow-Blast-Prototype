using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject boxPrefab;
    public Transform[] spawnPoints;
    public void SpawnBoxes(LevelData levelData)
    {
        int count = levelData.boxes.Length;

        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(boxPrefab, spawnPoints[i].position, Quaternion.identity);

            Box box = obj.GetComponent<Box>();
            box.color = levelData.boxes[i].color;
            box.target = 5;
        }
    }
}