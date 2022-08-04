using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WallRaycast : MonoBehaviour
{
    private BoxCollider _boxCollider;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void TurnOff()
    {
        _boxCollider.enabled = false;
    }
}
