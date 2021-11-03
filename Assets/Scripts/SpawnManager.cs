using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] public Ball targetBall;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform spawnParent;
    [SerializeField] public int spawnCount;
    [HideInInspector] public List<Ball> liveBalls = new List<Ball>();
    [HideInInspector] public List<Ball> deadBalls = new List<Ball>();
    [HideInInspector] private float xPos, yPos;

    public void BallSpawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            var xSpawnPoint = Random.Range(xPos, -xPos);
            var ySpawnPoint = Random.Range(yPos, -yPos);
            var zSpawnPoint = ballPrefab.transform.position.z;

            var ballBlowSpeed = Random.Range(0.2f, 0.5f);

            var colorRange = new Color(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f));

            var spawnPoint = new Vector3(xSpawnPoint, ySpawnPoint, zSpawnPoint);

            var go = Instantiate(ballPrefab, spawnPoint, Quaternion.identity, spawnParent);
            Ball bubbleBall = go.AddComponent<Ball>();
            bubbleBall.blowSpeed = ballBlowSpeed;
            bubbleBall.popSize = 2.0f;
            bubbleBall.color = colorRange;
            liveBalls.Add(bubbleBall);
        }

        targetBall = FindFastBall();
    }

    public Ball FindFastBall()
    {
        var bubbleBall = liveBalls[0];

        for (int i = 0; i < liveBalls.Count; i++)
        {
            if (bubbleBall.blowSpeed < liveBalls[i].blowSpeed)
            {
                bubbleBall = liveBalls[i];
            }
        }

        return bubbleBall;
    }

    public void ScreenAlign(float spawnOffset)
    {
        xPos = ((float)Screen.width / (float)Screen.height) * Camera.main.orthographicSize - spawnOffset;
        yPos = Camera.main.orthographicSize - spawnOffset;
    }
}
