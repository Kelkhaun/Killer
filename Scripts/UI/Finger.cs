using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class Finger : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] CameraFollower _cameraFollower;
    [SerializeField] Player _player;

    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public IEnumerator Show(float XPoistion1, float XPoistion2)
    {
        float duration1 = 0.01f;
        float duration2 = 1f;
        float delay1 = 0.5f;
        float delay2 = 1f;

        _rectTransform.DOAnchorPosX(XPoistion1, duration1);

        yield return new WaitForSeconds(delay1);
        _canvasGroup.alpha = 1;
        _rectTransform.DOAnchorPosX(XPoistion2, duration2);
        yield return new WaitForSeconds(delay2);
        _canvasGroup.alpha = 0;
    }

    public IEnumerator JumpToDirection(Vector2 startPosition, Vector2 targetPosition)
    {
        float delay1 = 0.6f;
        float delay2 = 2.5f;
        WaitForSeconds waitForSeconds1 = new WaitForSeconds(delay1);
        WaitForSeconds waitForSeconds2 = new WaitForSeconds(delay2);

        yield return waitForSeconds1;
        _canvasGroup.alpha = 1;
        _rectTransform.DOJumpAnchorPos(targetPosition, 1.5f, 1, 1f).SetLoops(2);
        _rectTransform.DOJumpAnchorPos(startPosition, 1, 1, 0.01f);
        yield return waitForSeconds2;
        _canvasGroup.alpha = 0;
    }
}
