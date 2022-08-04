using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class Finger : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvasGroup;

    private int _minimumValueAlpha = 0;
    private int _maximumValueAlpha = 1;
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
        _canvasGroup.alpha = _maximumValueAlpha;
        _rectTransform.DOAnchorPosX(XPoistion2, duration2);
        yield return new WaitForSeconds(delay2);
        _canvasGroup.alpha = _maximumValueAlpha;
    }

    public IEnumerator JumpToDirection(Vector2 startPosition, Vector2 targetPosition)
    {
        float delay1 = 0.6f;
        float delay2 = 2.5f;
        float duration1 = 1f;
        float duration2 = 0.01f;
        float jumpPower1 = 1.5f;
        float jumpPower2 = 1f;
        int jumpsNumber = 1;
        int repetitionsNumber = 2;

        WaitForSeconds waitForSeconds1 = new WaitForSeconds(delay1);
        WaitForSeconds waitForSeconds2 = new WaitForSeconds(delay2);

        yield return waitForSeconds1;
        _canvasGroup.alpha = _maximumValueAlpha;
        _rectTransform.DOJumpAnchorPos(targetPosition, jumpPower1, jumpsNumber, duration1).SetLoops(repetitionsNumber);
        _rectTransform.DOJumpAnchorPos(startPosition, jumpPower2, jumpsNumber, duration2);
        yield return waitForSeconds2;
        _canvasGroup.alpha = _minimumValueAlpha;
    }
}
