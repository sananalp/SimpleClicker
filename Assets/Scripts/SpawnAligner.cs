using UnityEngine;

public class SpawnAligner
{
    public void Align(out float xPos, out float yPos, float spawnOffset)
    {
        xPos = ((float)Screen.width / (float)Screen.height) * Camera.main.orthographicSize - spawnOffset;
        yPos = Camera.main.orthographicSize - spawnOffset;
    }
}
