using System.Collections;
using UnityEngine;

public class DottedLine : MonoBehaviour
{
    [SerializeField] private LayerMask _raycastMask;
    [SerializeField] private Transform _trailPoint;
    [SerializeField] private TrailRenderer _trailRenderer;

    private int _rayDistance = 30;
    private float _duration = 1.5f;

    private void Update()
    {
        if (Input.GetMouseButton(0))
            DrawLine();
    }

    private void DrawLine()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _rayDistance, _raycastMask))
        {
            float width = _trailRenderer.startWidth;
            float xPosition = 1f;
            float yPosition = 1f;
            _trailRenderer.material.mainTextureScale = new Vector2(xPosition / width, yPosition);
            _trailPoint.position = hit.point;
            _trailRenderer.enabled = true;
            StartCoroutine(DestroyLine());
        }
    }

    private IEnumerator DestroyLine()
    {
        WaitForSeconds waitForseconds = new WaitForSeconds(_duration);
        yield return waitForseconds;

        _trailRenderer.enabled = false;
        _trailRenderer.Clear();
    }
}
