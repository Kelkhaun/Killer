using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class Platform : MonoBehaviour
{
    [SerializeField] Island _nextIsland;
    [SerializeField] Decoration _decoration;
    [SerializeField] Enemy[] _enemy;
    [SerializeField] Priosoner _priosoner;
    [SerializeField] private List<WallRaycast> _wallsRaycast;

    private Vector3 _targetScale = Vector3.one;
    private float _duration = 0.4f;

    public void TurnOffWallCollider()
    {
        for (int i = 0; i < _wallsRaycast.Count; i++)
        {
            _wallsRaycast[i].TurnOff();
        }
    }

    public void SpawnPlatform()
    {
        for (int i = 0; i < _enemy.Length; i++)
        {
            _enemy[i].transform.DOScale(_targetScale, _duration);
        }

        _decoration.transform.DOScale(_targetScale, _duration);

        if (_nextIsland != null)
            _nextIsland.transform.DOScale(_targetScale, _duration);

        if (_priosoner != null)
            _priosoner.IncreseScale();
    }
}
