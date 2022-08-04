using System.Collections;
using UnityEngine;
using DG.Tweening;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
public class PlayerMover : MonoBehaviour
{
    private Animator _animator;
    private Player _player; 
    private int _motionState;
    private Vector3 _targetPosition1;
    private Vector3 _targetPosition2;

    private int _platformNumber;
    private int _firstPlatform = 1;
    private int _firstLevel = 1;
    private float _delay = 0.5f;

    public int PlatformNumber => _platformNumber;

    public event Action Running;

    private void OnEnable()
    {
        _player.AllEnemiesOnPlatformIsDead += OnAllEnemiesOnPlatformIsDead;
    }

    private void OnDisable()
    {
        _player.AllEnemiesOnPlatformIsDead -= OnAllEnemiesOnPlatformIsDead;
    }

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private IEnumerator Start()
    {
        _animator = GetComponent<Animator>();
        _platformNumber = 0;

        yield return new WaitForSeconds(_delay);

        if(SaveProgress.LevelNumber == _firstLevel)
        StartCoroutine(Move());
    }

    private void OnAllEnemiesOnPlatformIsDead(int emptyGroups, int emptyGroupsCount)
    {
        if (emptyGroups != emptyGroupsCount)
            StartCoroutine(Move());
    }

    private void Jump()
    {
        float jumpPower1 = 2;
        float jumpPower2 = 1;
        int jumpNumbers = 1;
        float duration1 = 1f;
        float duration2 = 1f;
        float zOffset1 = 8f;
        float zOffset2 = 7f;

        _targetPosition1 = new Vector3(transform.position.x, transform.position.y, transform.position.z + zOffset1);
        _targetPosition2 = new Vector3(transform.position.x, transform.position.y, transform.position.z + zOffset2);

        if (_platformNumber == _firstPlatform && SaveProgress.LevelNumber == _firstPlatform)
        {
            _animator.Play(PlayerAnimator.States.Jumping);
            transform.DOJump(_targetPosition1, jumpPower1, jumpNumbers, duration1);
        }
        else
        {
            _animator.Play(PlayerAnimator.States.Jumping2);
            transform.DOJump(_targetPosition2, jumpPower2, jumpNumbers, duration2);
        }

        _motionState = (int)AnimationState.Idle;
        _animator.SetInteger(PlayerAnimator.States.MotionState, _motionState);
    }

    private enum AnimationState
    {
        Idle = 0,
        Run = 1
    }

    private IEnumerator Move()
    {
        Running?.Invoke();
        _platformNumber++;
        float movementSpeed = 5f;
        float delay = 1.1f;
        float zOffset1 = 7.5f;
        float zOffset2 = 11f;
        WaitForSeconds waitForSecond = new WaitForSeconds(delay);

        yield return waitForSecond;
        _motionState = (int)AnimationState.Run;
        _animator.SetInteger(PlayerAnimator.States.MotionState, _motionState);

        if (_platformNumber == _firstPlatform && SaveProgress.LevelNumber == _firstLevel)
            _targetPosition1 = new Vector3(transform.position.x, transform.position.y, transform.position.z + zOffset1);
        else
            _targetPosition1 = new Vector3(transform.position.x, transform.position.y, transform.position.z + zOffset2);

        while (transform.position != _targetPosition1)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition1, movementSpeed * Time.deltaTime);
            yield return null;
        }

        Jump();
    }
}
