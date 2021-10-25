using System.Collections;
using UnityEngine;

class BubbleBall : MonoBehaviour
{
    public float BlowSpeed { get; set; }
    public float PopSize { get; set; }

    private float currentSize;
    private Vector3 ballSize;
    private Transform ballTransform;
    private SpriteRenderer ballSprite;

    private void Awake()
    {
        ballTransform = GetComponent<Transform>();
        ballSprite = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        StartCoroutine("BlowCoroutine");
    }
    public void BlowUp()
    {
        currentSize += Time.deltaTime * BlowSpeed;

        if (PopSize > currentSize)
        {
            ballSize = Vector3.one * currentSize;
            ballTransform.localScale = ballSize;
        }
        else BlowOut();
    }
    public void BlowOut()
    {
        Destroy(gameObject);
    }
    public void SetColor(Color color)
    {
        ballSprite.color = color;
    }

    IEnumerator BlowCoroutine()
    {
        while (true)
        {
            yield return null;
            BlowUp();
        }
    }
}