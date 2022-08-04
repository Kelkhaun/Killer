using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
public class Priosoner : MonoBehaviour
{
    [SerializeField] Player _player;

    private Animator _animator;
    private float _duration = 0.5f;

    private void OnEnable()
    {
        _player.AllEnemyDying += OnAllEnemyDying;
    }

    private void OnDisable()
    {
        _player.AllEnemyDying -= OnAllEnemyDying;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void IncreseScale()
    {
        transform.DOScale(Vector3.one, _duration);
    }

    private void OnAllEnemyDying()
    {
        _animator.Play(PriosonerAnimator.States.Celebration);
    }
}
