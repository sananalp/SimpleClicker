using UnityEngine;

abstract class Ball : MonoBehaviour
{
    public float ballBlowSpeed;
    public float ballPopTime;
    [HideInInspector] public float time;
    [HideInInspector] public Transform ballSize;

    public abstract void BlowUp();
    public abstract void BlowOut();
}
