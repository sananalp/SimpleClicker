using UnityEngine;

abstract class Ball : MonoBehaviour
{
    static public bool blowout;
    public float ballBlowSpeed;
    public float ballPopSize;
    private float currentSize;
    private Vector3 ballSize;
    protected Transform ball;

    private void Awake()
    {
        ball = GetComponent<Transform>();
    }
    protected void BlowUp()
    {
        currentSize += Time.deltaTime * ballBlowSpeed;

        if (ballPopSize > currentSize)
        {
            ballSize = new Vector3(currentSize, currentSize, currentSize);
            ball.localScale = ballSize;
        }
        else BlowOut();
    }
    private void BlowOut()
    {
        Destroy(gameObject);
        blowout = true;
    }
}