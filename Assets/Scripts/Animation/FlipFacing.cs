using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Transform))]
public class FlipFacing : MonoBehaviour
{
    Rigidbody2D _rigidbody;
    Transform _objectTransform;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _objectTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 _newScale = _objectTransform.localScale;
        if (_rigidbody.velocity.x > 0) _newScale.x = -Mathf.Abs(_objectTransform.localScale.x);
        else if (_rigidbody.velocity.x < 0) _newScale.x = Mathf.Abs(_objectTransform.localScale.x);
        transform.localScale = _newScale;
    }
}
