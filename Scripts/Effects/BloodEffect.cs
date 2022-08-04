using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class BloodEffect : MonoBehaviour
{
    [SerializeField] private Blot[] _blots;

    private ParticleSystem _particleSystem;
    private List<ParticleCollisionEvent> _collisionEvents = new List<ParticleCollisionEvent>();
    private List<Blot> _spawnedBlots = new List<Blot>();

    private int _minimumValue = 0;
    private float _delay = 0.5f;
    private float _duration = 1.5f;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = _particleSystem.GetCollisionEvents(other, _collisionEvents);

        SpawnBlots();
        StartCoroutine(HideBlots());
    }

    private void SpawnBlots()
    {
        for (int i = 0; i < _collisionEvents.Count; i++)
        {
            int randomIndex = Random.Range(_minimumValue, _blots.Length);
            Blot newBlot = Instantiate(_blots[randomIndex], _collisionEvents[i].intersection, _blots[i].transform.rotation, transform);
            _spawnedBlots.Add(newBlot);
        }
    }

    private IEnumerator HideBlots()
    {
        WaitForSeconds waitForseconds = new WaitForSeconds(_duration);
        yield return waitForseconds;

        for (int i = 0; i < _spawnedBlots.Count; i++)
        {
            _spawnedBlots[i].transform.DOScale(Vector3.zero, _delay);
        }
    }
}
