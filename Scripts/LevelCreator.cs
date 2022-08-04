using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private List<Platform> _platforms;
    [SerializeField] private Player _player;
    [SerializeField] private Island _islandAtStart;
    [SerializeField] private Decoration _decorationAtStart;
    [SerializeField] private Enemy[] _enemiesAtStart;

    private int _nextPlatform;
    private int _firstLevel = 1;
    private Vector3 _targetScale = Vector3.one;
    private float _duration = 0.4f;

    private void OnEnable()
    {
        _player.AllEnemiesOnPlatformIsDead += OnAllEnemiesOnPlatformIsDead;
    }

    private void OnDisable()
    {
        _player.AllEnemiesOnPlatformIsDead += OnAllEnemiesOnPlatformIsDead;
    }

    private void OnAllEnemiesOnPlatformIsDead(int emptyGroups, int enemiesGroupsCount)
    {
        _platforms[_nextPlatform].TurnOffWallCollider();

        if (enemiesGroupsCount != emptyGroups)
        SpawnNextPlatform();
    }

    private void Start()
    {
        _nextPlatform = 0;

        if (SaveProgress.LevelNumber == _firstLevel)
            StartCoroutine(SpawnSecondPlatform());
    }

    private void SpawnNextPlatform()
    {
        _platforms[_nextPlatform].SpawnPlatform();
        _nextPlatform++;
    }

    private IEnumerator SpawnSecondPlatform()
    {
        float delay = 0.9f;
        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);
        yield return waitForSeconds;

        _islandAtStart.transform.DOScale(_targetScale, _duration);
        _decorationAtStart.transform.DOScale(_targetScale, _duration);

        for (int i = 0; i < _enemiesAtStart.Length; i++)
        {
            _enemiesAtStart[i].transform.DOScale(_targetScale, _duration);
        }
    }
}
