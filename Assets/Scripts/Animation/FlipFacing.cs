// Written by Sage Mahmud

using UnityEngine;

public class FlipFacing : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    Transform _objectTransform;

    void Awake()
    {
        _rigidbody = GetComponentInParent<Rigidbody2D>();
        _objectTransform = GetComponent<Transform>();
    }

    void Update()
    {
        Vector2 _newScale = _objectTransform.localScale;
        if (_rigidbody.velocity.x > 0) _newScale.x = -Mathf.Abs(_objectTransform.localScale.x);
        else if (_rigidbody.velocity.x < 0) _newScale.x = Mathf.Abs(_objectTransform.localScale.x);
        transform.localScale = _newScale;
    }
}
