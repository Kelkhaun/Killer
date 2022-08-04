using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    const int ZeroEnemyCount = 0;

    [SerializeField] private EnemyGroup[] _enemiesGroups;
    [SerializeField] private LayerMask _raycastMask;
    [SerializeField] private Knife _knifePrefab;
    [SerializeField] private Transform _knifeSpawnPoint;
    [SerializeField] private Transform _knifeParent;

    private EnemyGroup _currentEnemyGroup;
    private PlayerMover _playerMover;
    private Animator _animator;
    private Knife _knife;
    private List<Vector3> _points = new List<Vector3>();
    private float _timeBetweenAddingPoints = 0.05f;
    private float _elapsedTime;
    private int _rayDistance = 30;
    private int _emptyGroups = 0;
    private int _firstLevel = 1;
    private int _secondPlatform = 2;
    private float _delay = 0f;
    private bool _canThrowKnife => _points.Count > 1;

    public int CurrentEnemyCount => _currentEnemyGroup.EnemyCount;

    public event Action PlayerAttacks;
    public event Action<int, int> AllEnemiesOnPlatformIsDead;
    public event Action AllEnemyDying;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(1) && _canThrowKnife)
            StartCoroutine(ThrowKnife());

        if (Input.GetMouseButton(0))
            DrawPath();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out EnemyGroup enemyGroup))
        {    
            _currentEnemyGroup = enemyGroup;
            _currentEnemyGroup.DeactivateCollider();
            _currentEnemyGroup.EnemyDying += OnEnemyDying;
        }
    }

    private void OnEnemyDying()
    {
        if (_currentEnemyGroup.EnemyCount == ZeroEnemyCount)
        {
            _emptyGroups++;
            AllEnemiesOnPlatformIsDead?.Invoke(_emptyGroups, _enemiesGroups.Length);
            _currentEnemyGroup.EnemyDying -= OnEnemyDying;
        }

        if (_emptyGroups == _enemiesGroups.Length)
            AllEnemyDying?.Invoke();
    }

    private void DrawPath()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _rayDistance, _raycastMask) && _elapsedTime > _timeBetweenAddingPoints)
        {
            _points.Add(hit.point);
            _elapsedTime = 0;
        }
    }

    private void SpawnKnife()
    {
        _knife = Instantiate(_knifePrefab, _knifeSpawnPoint.position, Quaternion.identity, _knifeParent);
    }

    private IEnumerator ThrowKnife()
    {
        SpawnKnife();
        PlayerAttacks?.Invoke();
        WaitForSeconds waitForSecond = new WaitForSeconds(_delay);

        if (_playerMover.PlatformNumber == _secondPlatform && SaveProgress.LevelNumber == _firstLevel)
            _animator.Play(PlayerAnimator.States.ThrowKnife2);
        else
            _animator.Play(PlayerAnimator.States.ThrowKnife);

        yield return waitForSecond;

        StartCoroutine(_knife.Move(_points));
    }
}
