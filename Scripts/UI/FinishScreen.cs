using UnityEngine;
using DG.Tweening;
using System;

public class FinishScreen : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] RectTransform[] _rectTransforms;

    private CanvasGroup _canvasGroup;
    private float _duration = 0.5f;

    public event Action FinishPanelOpen;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
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
        _canvasGroup.alpha = 1;

        for (int i = 0; i < _rectTransforms.Length; i++)
        {
            _rectTransforms[i].DOScale(Vector3.one, _duration);
        }
    }
}
