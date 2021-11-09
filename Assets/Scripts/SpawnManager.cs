using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Vector3 spawnPoint;
    private float ballBlowSpeed;
    private Color colorRange;
    [HideInInspector] public Ball targetBall;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform spawnParent;
    public int spawnCount;
    [SerializeField] private float spawnOffset;
    [HideInInspector] public List<Ball> liveBalls = new List<Ball>();
    [HideInInspector] public List<Ball> deadBalls = new List<Ball>();

    private void Init()
    {
        float xPos, yPos;
        SpawnAligner spawnAligner = new SpawnAligner();
        spawnAligner.Align(out xPos, out yPos, spawnOffset);

        var xSpawnPoint = Random.Range(xPos, -xPos);
        var ySpawnPoint = Random.Range(yPos, -yPos);
        var zSpawnPoint = ballPrefab.transform.position.z;

        ballBlowSpeed = Random.Range(0.2f, 0.5f);
        colorRange = new Color(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f));
        spawnPoint = new Vector3(xSpawnPoint, ySpawnPoint, zSpawnPoint);
    }
    public void BallSpawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Init();
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
}