using UnityEngine;
using DG.Tweening;
using TMPro;

[RequireComponent(typeof(RectTransform))]
public class Frame : MonoBehaviour
{
    private const string Gratitude = "THANKS!";

    [SerializeField] private Player _player;

    private TMP_Text _text;
    private RectTransform _rectTransform;
    private float _duration = 1f;

    private void OnEnable()
    {
        _rectTransform = GetComponent<RectTransform>();
        _text = GetComponentInChildren<TMP_Text>();
        _player.AllEnemyDying += OnAllEnemyDying;
        _player.AllEnemiesOnPlatformIsDead += OnAllEnemiesOnPlatformIsDead;
    }

    private void OnDisable()
    {
        _player.AllEnemyDying -= OnAllEnemyDying;
        _player.AllEnemiesOnPlatformIsDead -= OnAllEnemiesOnPlatformIsDead;
    }

    private void OnAllEnemiesOnPlatformIsDead(int emptyGroups, int emptyGroupsCount)
    {
        _rectTransform.DOScale(Vector3.one, _duration);
    }

    private void OnAllEnemyDying()
    {
        _text.text = Gratitude;
    }
}
