using UnityEngine;
using DG.Tweening;

public class UIAnimator : MonoBehaviour
{
    [Header("UI elements")]
    [SerializeField] private RectTransform scoreText;
    [SerializeField] private RectTransform restartButton;
    [SerializeField] private RectTransform playButton;
    [SerializeField] private RectTransform chooseModePage;

    public void PlayButtonEnableAnim()
    {
        playButton
            .DOScale(Vector3.one, 1.0f)
            .SetEase(Ease.OutBounce);
    }
    public void PlayButtonDisableAnim()
    {
        playButton
            .DOScale(Vector3.zero, 0.2f)
            .SetEase(Ease.Linear);
    }
    public void ChoosePageEnableAnim()
    {
        chooseModePage
            .DOAnchorPosY(0, 1.0f)
            .SetEase(Ease.OutBounce);
    }
    public void ChoosePageDisableAnim()
    {
        chooseModePage
            .DOAnchorPosY(902.0f, 0.2f)
            .SetEase(Ease.Linear);
    }
    public void ScoreTextEnableAnim()
    {
        scoreText
            .DOScale(Vector3.one, 1.0f)
            .SetEase(Ease.OutBounce);
    }
    public void RestartButtonEnableAnim()
    {
        restartButton
            .DOScale(Vector3.one, 1.0f)
            .SetEase(Ease.OutBounce);
    }
    public void RestartButtonDisableAnim()
    {
        restartButton
            .DOScale(Vector3.zero, 0.2f)
            .SetEase(Ease.Linear);
    }
}