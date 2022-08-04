using UnityEngine;
using DG.Tweening;
using System;

public class FinishScreen : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] RectTransform[] _rectTransforms;

    private CanvasGroup _canvasGroup;
    private float _duration = 0.5f;
    private int _minimumValueAlpha = 0;
    private int _maximumValueAlpha = 1;

    public event Action FinishPanelOpen;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = _minimumValueAlpha;
    }

    private void OnEnable()
    {
        _player.AllEnemyDying += OnAllEnemyDying;
    }

    private void OnDisable()
    {
        _player.AllEnemyDying -= OnAllEnemyDying;
    }

    private void OnAllEnemyDying()
    {
        FinishPanelOpen?.Invoke();
        _canvasGroup.alpha = _maximumValueAlpha;

        for (int i = 0; i < _rectTransforms.Length; i++)
        {
            _rectTransforms[i].DOScale(Vector3.one, _duration);
        }
    }
}
