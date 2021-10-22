using UnityEngine;

abstract class Ball : MonoBehaviour
{
    static public bool blowout;
    public float ballBlowSpeed;
    public float ballPopSize;
    private float currentSize;
    private Vector3 ballSize;
    private Transform ballTransform;
    private SpriteRenderer ballSprite;

    private void Awake()
    {
        ballTransform = GetComponent<Transform>();
        ballSprite = GetComponent<SpriteRenderer>();
    }
    protected void BlowUp()
    {
        currentSize += Time.deltaTime * ballBlowSpeed;

        if (ballPopSize > currentSize)
        {
            ballSize = new Vector3(currentSize, currentSize, currentSize);
            ballTransform.localScale = ballSize;
        }
        else BlowOut();
    }
    private void BlowOut()
    {
        Destroy(gameObject);
        blowout = true;
    }
    public void SetColor(Color color)
    {
        ballSprite.color = color;
    }
}