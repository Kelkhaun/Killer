using System;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(EnemyPhysics))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _rigidbodies;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
    [SerializeField] private MeshRenderer[] _meshRenderers;
    [SerializeField] private bool _isStartScaleIncrease;

    private EnemyPhysics _enemyPhysics;
    private Animator _animator;
    private BoxCollider _collider;
    private float _duration = 0.5f;

    public event Action<Enemy> OneEnemyIsDead;

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _enemyPhysics = GetComponent<EnemyPhysics>();
        _animator = GetComponent<Animator>();

        if (_isStartScaleIncrease == true)
            IncreseScale();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out Knife knife))
            Die();

        if (collider.gameObject.TryGetComponent(out Box box))
        {
            box.MakePhysical();
            Die();
        }
    }

    private void IncreseScale()
    {
        transform.DOScale(Vector3.one, _duration);
    }

    private void ChangeColor()
    {
        float colorShade1 = 0.345f;
        float colorShade2 = 0.372f;
        float colorShade3 = 0.239f;

        Color mainColor = new Color(colorShade1, colorShade1, colorShade1);
        Color colorDim = new Color(colorShade2, colorShade2, colorShade2);
        Color flatRimColor = new Color(colorShade3, colorShade3, colorShade3);


        _skinnedMeshRenderer.material.SetColor("_Color", mainColor);
        _skinnedMeshRenderer.material.SetColor("_ColorDim", colorDim);
        _skinnedMeshRenderer.material.SetColor("_FlatRimColor", flatRimColor);

        if (_meshRenderers != null)
        {
            for (int i = 0; i < _meshRenderers.Length; i++)
            {
                _meshRenderers[i].material.SetColor("_Color", mainColor);
                _meshRenderers[i].material.SetColor("_ColorDim", colorDim);
                _meshRenderers[i].material.SetColor("_FlatRimColor", flatRimColor);
            }
        }
    }

    private void Die()
    {
        _animator.enabled = false;
        _collider.enabled = false;
        OneEnemyIsDead?.Invoke(this);
        _enemyPhysics.ReassignBones();
        _particleSystem.Play();
        ChangeColor();

        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].isKinematic = false;
        }
    }
}
