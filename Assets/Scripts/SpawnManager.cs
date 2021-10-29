using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<BubbleBall> liveBalls = new List<BubbleBall>();
    public List<BubbleBall> deadBalls = new List<BubbleBall>();
    private float xPos, yPos;

    public void BallSpawn(GameObject ballPrefab, Transform spawnParent, int ballSpawnCount)
    {
        for (int i = 0; i < ballSpawnCount; i++)
        {
            var xSpawnPoint = Random.Range(xPos, -xPos);
            var ySpawnPoint = Random.Range(yPos, -yPos);
            var zSpawnPoint = ballPrefab.transform.position.z;

            var ballBlowSpeed = Random.Range(0.2f, 0.5f);

            var colorRange = new Color(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f));

            var spawnPoint = new Vector3(xSpawnPoint, ySpawnPoint, zSpawnPoint);

            var go = Instantiate(ballPrefab, spawnPoint, Quaternion.identity, spawnParent);
            BubbleBall bubbleBall = go.AddComponent<BubbleBall>();
            bubbleBall.BlowSpeed = ballBlowSpeed;
            bubbleBall.PopSize = 2.0f;
            bubbleBall.Color = colorRange;
            liveBalls.Add(bubbleBall);
        }
    }
    public void ScreenAlign(float spawnOffset)
    {
        xPos = ((float)Screen.width / (float)Screen.height) * Camera.main.orthographicSize - spawnOffset;
        yPos = Camera.main.orthographicSize - spawnOffset;
    }
}
