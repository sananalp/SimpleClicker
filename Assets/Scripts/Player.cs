using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public Camera cameraVision { get; private set; }
    [SerializeField] private Game Game;

    private void Start()
    {
        cameraVision = GetComponentInChildren<Camera>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            OnBallClick();
        }
    }
    private void OnBallClick()
    {
        var ray = cameraVision.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.TryGetComponent(out Ball ball))
        {
            Game.PopBall(ball);
            Game.PopEffect(ball);
            Game.PopSound();
            Game.AddScore();
            Game.ShowScore();
        }
    }
}