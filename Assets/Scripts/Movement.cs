// Written by Sage Mahmud

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [Header("References")]
    Rigidbody2D _rigidbody;

    [Header("Attributes")]
    [SerializeField] protected float speed = 4f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        _rigidbody.velocity = direction * speed;
    }
}
