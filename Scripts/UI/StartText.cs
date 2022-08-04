using UnityEngine;

public class StartText : MonoBehaviour
{
    [SerializeField] Player _player;

    private void OnEnable()
    {
        _player.PlayerAttacks += OnPlayerAttack;
    }

    private void OnDisable()
    {
        _player.PlayerAttacks -= OnPlayerAttack;
    }

    private void OnPlayerAttack()
    {
        gameObject.active = false;
    }

}
