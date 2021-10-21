using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Object properties")]
    [SerializeField] GameObject ballPrefab;
    List<Ball> balls = new List<Ball>();

    [Header("Object spawn properties")]
    public Transform spawnParent;
    Vector3 spawnPoint;
    public int ballSpawnCount;
    public float spawnOffset;

    [Header("UI Elements")]
    [SerializeField] Text scoreText;
    [SerializeField] Button restartButton;

    int score;
    float xPos, yPos;

    void Start()
    {
        ScreenAlign();
        BallSpawn();
    }

    void Update()
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

        if (balls.Count == 0)
        {
            NextLevel();
            BallSpawn();
        }
        else if (Ball.blowout)
        {
            GameOver();
        }
    }
    void BallSpawn()
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
    void BallDestroy(RaycastHit hit)
    {
        Destroy(hit.collider.gameObject);
        balls.Remove(hit.collider.GetComponent<Ball>());
    }
    void AddScore()
    {
        score++;
    }
    void ShowScore()
    {
        scoreText.resizeTextMaxSize = 200;
        scoreText.text = score.ToString();
    }
    void NextLevel()
    {
        ballSpawnCount++;
    }
    void ScreenAlign()
    {
        xPos = ((float)Screen.width / (float)Screen.height) * Camera.main.orthographicSize - spawnOffset;
        yPos = Camera.main.orthographicSize - spawnOffset;
    }
    void GameOver()
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
}