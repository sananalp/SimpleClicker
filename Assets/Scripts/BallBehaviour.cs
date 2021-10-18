using UnityEngine;

class BallBehaviour : Ball
{
    public float time;

    void Start()
    {
        ballBlowSpeed = 0.2f;
        ballPopTime = 2.0f;
        ballSize = GetComponent<Transform>();
    }

    void Update()
    {
        time += Time.deltaTime * ballBlowSpeed;

        if (ballPopTime > time)
        {
            ballSize.localScale = new Vector3(time, time);
            
        }
        else Destroy(gameObject);
    }
}