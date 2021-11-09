using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [Header("Player properties")]
    [HideInInspector] private int score;

    [Header("Game Managers")]
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private UIAnimator animator;

    [Header("UI elements")]
    [SerializeField] private Text scoreText;

    [Header("Audio elements")]
    [SerializeField] private AudioSource ballPopSound;

    [Header("VFX Elements")]
    [SerializeField] private ParticleSystem ballPopParticle;

    private void Start()
    {
        animator.PlayButtonEnableAnim();
    }
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
        score = 0;
        spawnManager.spawnCount = 1;
        ShowScore();
        spawnManager.BallSpawn();
        animator.ScoreTextEnableAnim();
        animator.RestartButtonDisableAnim();
    }
    public void Play()
    {
        animator.ChoosePageEnableAnim();
        animator.PlayButtonDisableAnim();
    }
    public void WaveModePlay()
    {
        spawnManager.BallSpawn();
        animator.ScoreTextEnableAnim();
        animator.ChoosePageDisableAnim();
    }
    private void NextLevel()
    {
        spawnManager.spawnCount++;
        DestroyAllObjects(spawnManager.deadBalls);
    }
    private void GameOver()
    {
        animator.RestartButtonEnableAnim();
        scoreText.resizeTextMaxSize = 80;
        scoreText.text = $"Game Over!\nYour score: {score}";
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