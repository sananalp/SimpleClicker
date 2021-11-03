using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Player properties")]
    [HideInInspector] private int score;

    [Header("Game Managers")]
    [SerializeField] private SpawnManager spawnManager;

    [Header("Spawn properties")]
    [SerializeField] private float spawnOffset;

    [Header("UI elements")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button playButton;

    [Header("Audio elements")]
    [SerializeField] private AudioSource ballPopSound;

    [Header("VFX Elements")]
    [SerializeField] private ParticleSystem ballPopParticle;

    private void Update()
    {
        if (spawnManager.targetBall != null && spawnManager.targetBall.currentSize > spawnManager.targetBall.popSize)
            GameOver();
    }

    public void PopBall(Ball bubbleBall)
    {
        spawnManager.deadBalls.Add(bubbleBall);
        spawnManager.liveBalls.Remove(bubbleBall);
        bubbleBall.gameObject.SetActive(false);

        if (spawnManager.liveBalls.Count == 0)
        {
            NextLevel();
            spawnManager.BallSpawn();
        }

        spawnManager.targetBall = spawnManager.FindFastBall();
    }
    public void PopEffect(Ball bubbleBall)
    {
        var particle = Instantiate(ballPopParticle, bubbleBall.transform.position, Quaternion.identity);
        var particleMain = particle.main;
        particleMain.startColor = bubbleBall.color;
        particle.transform.localScale = bubbleBall.size;
        particle.Play();
    }
    public void PopSound()
    {
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
        restartButton.gameObject.SetActive(false);
        score = 0;
        spawnManager.spawnCount = 1;
        ShowScore();
        spawnManager.BallSpawn();
    }
    public void Play()
    {
        scoreText.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
        spawnManager.ScreenAlign(spawnOffset);
        spawnManager.BallSpawn();
    }

    private void NextLevel()
    {
        spawnManager.spawnCount++;
        DestroyAllObjects(spawnManager.deadBalls);
    }
    private void GameOver()
    {
        scoreText.resizeTextMaxSize = 80;
        scoreText.text = $"Game Over!\nYour score: {score}";
        restartButton.gameObject.SetActive(true);
        DestroyAllObjects(spawnManager.liveBalls);
        DestroyAllObjects(spawnManager.deadBalls);
    }
    private void DestroyAllObjects(List<Ball> list)
    {
        foreach (Ball bubbleBall in list)
        {
            Destroy(bubbleBall.gameObject);
        }
        list.Clear();
    }
}