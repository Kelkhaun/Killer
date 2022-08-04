using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rope : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.TryGetComponent(out Knife knife))
        {
            _rigidbody.isKinematic = false;
        }
    }
}
