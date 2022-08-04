using System.Collections;
using UnityEngine;
using Cinemachine;
using System;

public class CameraFollower : MonoBehaviour
{
    const int ZeroEnemy = 0;

    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private Player _player;
    [SerializeField] private Finger _finger1;
    [SerializeField] private Finger _finger2;
    [SerializeField] private Frame _helpPanel;

    private float _XPoistion1 = -541.44f;
    private float _XPoistion2 = -537.9f;
    private int _firstLevel = 1;
    private int _secondPlatform = 2;
    private int _firstPlatform = 1;
    private float _delay1 = 2.3f;
    private float _delay2 = 3f;
    private float _delay3 = 1f;

    private void OnEnable()
    {
        _player.PlayerAttacks += OnPlayersAttacks;
        _playerMover.Running += OnRunning;
    }

    private void OnDisable()
    {
        _player.PlayerAttacks -= OnPlayersAttacks;
        _playerMover.Running -= OnRunning;
    }

    private void OnRunning()
    {
        StartCoroutine(MoveToPlatform());
    }

    private void OnPlayersAttacks()
    {
        StartCoroutine(ShowAttack());
    }

    private IEnumerator ShowAttack()
    {
        Vector3 targetPosition1 = new Vector3(0, 3, 4.84f);
        Quaternion targetRotation1 = Quaternion.Euler(12f, 0, 0);
        Vector3 targetPosition2 = new Vector3(0, 1, -1.3f);
        Quaternion targetRotation2 = Quaternion.Euler(0, 0, 0);

        StartCoroutine(Move(targetPosition1));
        StartCoroutine(Turn(targetRotation1));

        yield return new WaitForSeconds(2f);

        if (_playerMover.PlatformNumber == _firstPlatform && SaveProgress.LevelNumber == _firstLevel)
            StartCoroutine(_finger1.Show(_XPoistion1, _XPoistion2));

        if (_player.CurrentEnemyCount == ZeroEnemy)
        {
            StartCoroutine(Move(targetPosition1));
            StartCoroutine(Turn(targetRotation1));
        }
        else
        {
            StartCoroutine(Move(targetPosition2));
            StartCoroutine(Turn(targetRotation2));
        }
    }

    private IEnumerator MoveToPlatform()
    {
        yield return new WaitForSeconds(_delay3);

        Vector3 targetPosition1 = new Vector3(0, 3, 6);
        Vector3 targetPosition2 = new Vector3(0, 1, -1.3f);
        Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
        Vector2 targetPosition3 = new Vector2(-538.69f, -1882.602f);
        Vector2 startPosition = new Vector2(-540.8f, -1882.602f);

        StartCoroutine(Move(targetPosition1));

        if (_playerMover.PlatformNumber == _firstPlatform && SaveProgress.LevelNumber == _firstLevel)
            yield return new WaitForSeconds(_delay1);
        else
            yield return new WaitForSeconds(_delay2);

        StartCoroutine(Move(targetPosition2));
        StartCoroutine(Turn(targetRotation));

        if (_playerMover.PlatformNumber == _firstPlatform && SaveProgress.LevelNumber == _firstLevel)
            StartCoroutine(_finger1.Show(_XPoistion1, _XPoistion2));
        else if (_playerMover.PlatformNumber == _secondPlatform && SaveProgress.LevelNumber == _firstLevel)
            StartCoroutine(_finger2.JumpToDirection(startPosition, targetPosition3));
    }

    private IEnumerator Move(Vector3 targetPoistion)
    {
        float duration = 30f;

        while (_camera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset != targetPoistion)
        {
            _camera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.MoveTowards(_camera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, targetPoistion, duration * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Turn(Quaternion targetRotation)
    {
        float duration = 50f;

        while (_camera.transform.rotation != targetRotation)
        {
            _camera.transform.rotation = Quaternion.RotateTowards(_camera.transform.rotation, targetRotation, duration * Time.deltaTime);
            yield return null;
        }
    }
}