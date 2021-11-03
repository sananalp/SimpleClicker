using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float blowSpeed { get; set; }
    public float popSize { get; set; }
    public Color color { get { return spriteRenderer.color; } set { spriteRenderer.color = value; } }
    public Transform BallTransform { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    public Vector3 size { get; private set; }
    public float currentSize { get; private set; }

    private void Awake()
    {
        BallTransform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(BlowCoroutine());
    }
    private void Inflate()
    {
        currentSize += Time.deltaTime * blowSpeed;

        if (popSize > currentSize)
        {
            size = Vector3.one * currentSize;
            BallTransform.localScale = size;
        }
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