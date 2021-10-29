using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Player properties")]
    private int score;
    private bool checkPlay;

    [Header("Game Managers")]
    private SpawnManager spawnManager;

    [Header("Spawn properties")]
    [SerializeField] private int ballSpawnCount;
    [SerializeField] private float spawnOffset;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private GameObject ballPrefab;

    [Header("UI elements")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button playButton;

    [Header("Audio elements")]
    [SerializeField] private AudioSource ballPopSound;

    private void Start()
    {
        spawnManager = gameObject.AddComponent<SpawnManager>();
    }

    private void Update()
    {
        if (spawnManager.liveBalls.Count == 0 && checkPlay)
        {
            NextLevel();
            spawnManager.BallSpawn(ballPrefab, spawnParent, ballSpawnCount);
            Debug.Log("Next Level");
        }

        else if (BubbleBall.SizeOut)
        {
            GameOver();
            Debug.Log("Game Over");
        }
    }
    public void PopBall(BubbleBall ball)
    {
        spawnManager.deadBalls.Add(ball);
        spawnManager.liveBalls.Remove(ball);
        ball.gameObject.SetActive(false);
        ballPopSound.Play();
    }
    public void AddScore()
    {
        score++;
    }
    public void ShowScore()
    {
        scoreText.resizeTextMaxSize = 200;
        scoreText.text = score.ToString();
    }
    public void Restart()
    {
        checkPlay = true;
        restartButton.gameObject.SetActive(false);
        score = 0;
        ballSpawnCount = 0;
        ShowScore();
    }
    public void Play()
    {
        checkPlay = true;
        scoreText.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
        spawnManager.BallSpawn(ballPrefab, spawnParent, ballSpawnCount);
        spawnManager.ScreenAlign(spawnOffset);
    }

    private void NextLevel()
    {
        ballSpawnCount++;
        DestroyAllObjects(spawnManager.deadBalls);
    }
    private void GameOver()
    {
        checkPlay = false;
        BubbleBall.SizeOut = false;
        scoreText.resizeTextMaxSize = 80;
        scoreText.text = $"Game Over!\nYour score: {score}";
        restartButton.gameObject.SetActive(true);
        DestroyAllObjects(spawnManager.liveBalls);
        DestroyAllObjects(spawnManager.deadBalls);
    }
    private void DestroyAllObjects(List<BubbleBall> list)
    {
        foreach (BubbleBall ball in list)
        {
            Destroy(ball.gameObject);
        }
        list.Clear();
    }
}