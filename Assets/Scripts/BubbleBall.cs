using UnityEngine;

class BubbleBall : Ball
{
    void Start()
    {
        ballSize = GetComponent<Transform>();
    }
    void Update()
    {
        BlowUp();
    }
    public override void BlowUp()
    {
        time += Time.deltaTime * ballBlowSpeed;

        if (ballPopTime > time)
        {
            ballSize.localScale = new Vector3(time, time);

        }
        else BlowOut();
    }
    public override void BlowOut()
    {
        Destroy(gameObject);
    }
}