using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Object properties")]
    public GameObject ballPrefab;

    [Header("Object spawn properties")]
    public Transform spawnParent;
    Vector3 spawnPoint;
    public int maxBallCount;
    public float spawnOffset;

    [Header("UI Elements")]
    [SerializeField]
    Text scoreText;

    int score;
    float xPos, yPos;

    void Start()
    {
        xPos = ((float)Screen.width / (float)Screen.height) * Camera.main.orthographicSize - spawnOffset;
        yPos = Camera.main.orthographicSize - spawnOffset;
    }

    void Update()
    {
        if (spawnParent.childCount == 0)
        {
            NextLevel();
            BallSpawn();
        }

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
    }
    void BallSpawn()
    {
        for (int i = 0; i < maxBallCount; i++)
        {
            spawnPoint = new Vector3(Random.Range(xPos,-xPos), Random.Range(yPos,-yPos), ballPrefab.transform.position.z);

            Instantiate(ballPrefab, spawnPoint, Quaternion.identity, spawnParent);
        }
    }
    void BallDestroy(RaycastHit hit)
    {
        Destroy(hit.collider.gameObject);
    }
    void AddScore()
    {
        score++;
    }
    void ShowScore()
    {
        scoreText.text = score.ToString();
    }
    void NextLevel()
    {
        maxBallCount++;
    }
}