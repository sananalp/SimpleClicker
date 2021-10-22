using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Player properties")]
    [SerializeField] private int ballSpawnCount;
    [SerializeField] private float spawnOffset;
    private int score;
    private float xPos, yPos;

    [Header("Ball properties")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private AudioSource ballPopSound;
    List<Ball> balls = new List<Ball>();

    [Header("Spawn properties")]
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
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "HitObject")
            {
                BallDestroy(hit);
                AddScore();
                ShowScore();
            }
        }

        if (balls.Count == 0 && scoreText.IsActive())
        {
            NextLevel();
            BallSpawn();
        }
        else if (Ball.blowout)
        {
            GameOver();
        }
    }

    private void BallSpawn()
    {
        for (int i = 0; i < ballSpawnCount; i++)
        {
            spawnPoint = new Vector3(Random.Range(xPos,-xPos), Random.Range(yPos,-yPos), ballPrefab.transform.position.z);

            var go = Instantiate(ballPrefab, spawnPoint, Quaternion.identity, spawnParent);
            Ball bubbleBall = go.AddComponent<BubbleBall>();
            bubbleBall.ballBlowSpeed = Random.Range(0.2f, 0.6f);
            bubbleBall.ballPopSize = 2.0f;
            bubbleBall.SetColor(new Color(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f)));
            balls.Add(bubbleBall);
        }
    }
    private void BallDestroy(RaycastHit hit)
    {
        Destroy(hit.collider.gameObject);
        balls.Remove(hit.collider.GetComponent<Ball>());
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
        scoreText.resizeTextMaxSize = 80;
        scoreText.text = $"Game Over!\nYour score: {score}";
        restartButton.gameObject.SetActive(true);

        foreach(Ball ball in spawnParent.GetComponentsInChildren<BubbleBall>())
        {
            Destroy(ball.gameObject);
        }
    }
    public void Restart()
    {
        restartButton.gameObject.SetActive(false);
        score = 0;
        ballSpawnCount = 0;
        ShowScore();
        balls.Clear();
        Ball.blowout = false;
    }
    public void Play()
    {
        scoreText.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
        ScreenAlign();
        BallSpawn();
    }
}