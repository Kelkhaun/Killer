using UnityEngine;
using RayFire;

[RequireComponent(typeof(RayfireRigid))]
public class Box : MonoBehaviour
{
    RayfireRigid _rayFire;

    private void Start()
    {
        _rayFire = GetComponent<RayfireRigid>();
    }

    public void MakePhysical()
    {
        transform.parent = null;

        _rayFire.Demolish();
    }
}
