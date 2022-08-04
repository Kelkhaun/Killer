using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using static DG.Tweening.DOTweenCYInstruction;

public class Knife : MonoBehaviour
{
    [SerializeField] private GameObject _knifeModel;
    [SerializeField] private bool _isRotate;

    private Tween _knifeMovement;
    private bool _isWorking = false;
    private float _duration1 = 1f;
    private Vector3 _threeSixteenths = new Vector3(0, 360, 0);
    private float _duration2 = 2f;

    private void Update()
    {
        if (_isWorking == true)
            _knifeModel.transform.DORotate(_threeSixteenths, _duration2, RotateMode.LocalAxisAdd).SetLoops(-1);
    }

    public IEnumerator Move(List<Vector3> points)
    {
        float duration = 1.1f;

        if (_isRotate == true)
            _isWorking = true;

        transform.parent = null;
        _knifeMovement = transform.DOPath(points.ToArray(), duration, PathType.Linear).SetAutoKill();

        yield return new WaitForCompletion(_knifeMovement);

        StartCoroutine(Deactivate(points));
    }

    private IEnumerator Deactivate(List<Vector3> points)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_duration1);

        points.Clear();
        gameObject.SetActive(false);
        yield return waitForSeconds;
        Destroy(gameObject);
    }
}