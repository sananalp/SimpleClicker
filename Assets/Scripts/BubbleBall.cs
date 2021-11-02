using System.Collections;
using UnityEngine;

public class BubbleBall : MonoBehaviour
{
    static public bool SizeOut { get; set; }
    public float BlowSpeed { get; set; }
    public float PopSize { get; set; }
    public Color Color { get { return ballSprite.color; } set { ballSprite.color = value; } }
    public Transform ballTransform { get; private set; }
    public SpriteRenderer ballSprite { get; private set; }

    private float currentSize;
    private Vector3 ballSize;

    private void Awake()
    {
        ballTransform = GetComponent<Transform>();
        ballSprite = GetComponent<SpriteRenderer>();
        StartCoroutine(BlowCoroutine());
    }
    private void Inflate()
    {
        currentSize += Time.deltaTime * BlowSpeed;

        if (PopSize > currentSize)
        {
            ballSize = Vector3.one * currentSize;
            ballTransform.localScale = ballSize;
        }
        else SizeOut = true;
    }

    IEnumerator BlowCoroutine()
    {
        while (true)
        {
            yield return null;
            Inflate();
        }
    }
}