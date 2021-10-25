using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Player properties")]
    [SerializeField] private Camera playerCamera;
    private int score;
    private float xPos, yPos;
    private bool checkPlay;
    private bool checkLose;

    [Header("Ball properties")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private AudioSource ballPopSound;
    [SerializeField] List<BubbleBall> balls = new List<BubbleBall>();

    [Header("Spawn properties")]
    [SerializeField] private int ballSpawnCount;
    [SerializeField] private float spawnOffset;
    [SerializeField] private Transform spawnParent;
    private Vector3 spawnPoint;

    [Header("UI elements")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button playButton;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.TryGetComponent(out BubbleBall ball))
            {
                BallDestroy(ball);
                AddScore();
                ShowScore();
            }
        }

        if (balls.Count == 0 && checkPlay)
        {
            NextLevel();
            BallSpawn();
        }
        else if(checkLose == false)
        {
            foreach(BubbleBall ball in balls)
            {
                if (ball == null) checkLose = true;
            }
        }
        else
        {
            GameOver();
        }
    }

    private void BallSpawn()
    {
        for (int i = 0; i < ballSpawnCount; i++)
        {
            var xSpawnPoint = Random.Range(xPos, -xPos);
            var ySpawnPoint = Random.Range(yPos, -yPos);
            var zSpawnPoint = ballPrefab.transform.position.z;

            var ballBlowSpeed = Random.Range(0.2f, 0.5f);
            var ballPopSize = 2.0f;

            var colorRange = new Color(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f));

            spawnPoint = new Vector3(xSpawnPoint, ySpawnPoint, zSpawnPoint);

            var go = Instantiate(ballPrefab, spawnPoint, Quaternion.identity, spawnParent);
            BubbleBall bubbleBall = go.AddComponent<BubbleBall>();
            bubbleBall.BlowSpeed = ballBlowSpeed;
            bubbleBall.PopSize = ballPopSize;
            bubbleBall.SetColor(colorRange);
            balls.Add(bubbleBall);
        }
    }
    private void BallDestroy(BubbleBall ball)
    {
        balls.Remove(ball);
        Destroy(ball.gameObject);
        ballPopSound.Play();
    }
    private void AddScore()
    {
        score++;
    }
    private void ShowScore()
    {
        scoreText.resizeTextMaxSize = 200;
        scoreText.text = score.ToString();
    }
    private void NextLevel()
    {
        ballSpawnCount++;
    }
    private void ScreenAlign()
    {
        xPos = ((float)Screen.width / (float)Screen.height) * Camera.main.orthographicSize - spawnOffset;
        yPos = Camera.main.orthographicSize - spawnOffset;
    }
    private void GameOver()
    {
        checkPlay = false;
        scoreText.resizeTextMaxSize = 80;
        scoreText.text = $"Game Over!\nYour score: {score}";
        restartButton.gameObject.SetActive(true);

        foreach(BubbleBall ball in spawnParent.GetComponentsInChildren<BubbleBall>())
        {
            Destroy(ball.gameObject);
        }
    }
    public void Restart()
    {
        checkPlay = true;
        checkLose = false;
        restartButton.gameObject.SetActive(false);
        score = 0;
        ballSpawnCount = 0;
        ShowScore();
        balls.Clear();
    }
    public void Play()
    {
        checkPlay = true;
        scoreText.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
        ScreenAlign();
        BallSpawn();
    }
}