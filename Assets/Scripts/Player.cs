using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameController gameController;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            OnBallClick();
        }
    }
    private void OnBallClick()
    {
        var ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.TryGetComponent(out Ball ball))
        {
            gameController.PopBall(ball);
            gameController.PopEffect(ball);
            gameController.PopSound();
            gameController.AddScore();
            gameController.ShowScore();
        }
    }
}