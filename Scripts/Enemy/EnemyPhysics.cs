using UnityEngine;

public class EnemyPhysics : MonoBehaviour
{
    private Transform[] _children;
    private Vector3[] _connectedAnchor;
    private Vector3[] _anchor;

    private void Start()
    {
        HashAnchorValues();
    }

    public void ReassignBones()
    {
        for (int i = 0; i < _children.Length; i++)
        {
            if (_children[i].GetComponent<CharacterJoint>() != null)
            {
                _children[i].GetComponent<CharacterJoint>().connectedAnchor = _connectedAnchor[i];
                _children[i].GetComponent<CharacterJoint>().anchor = _anchor[i];
            }
        }
    }

    private void HashAnchorValues()
    {
        _children = transform.GetComponentsInChildren<Transform>();
        _connectedAnchor = new Vector3[_children.Length];
        _anchor = new Vector3[_children.Length];

        for (int i = 0; i < _children.Length; i++)
        {
            if (_children[i].GetComponent<CharacterJoint>() != null)
            {
                _connectedAnchor[i] = _children[i].GetComponent<CharacterJoint>().connectedAnchor;
                _anchor[i] = _children[i].GetComponent<CharacterJoint>().anchor;
            }
        }
    }
}
