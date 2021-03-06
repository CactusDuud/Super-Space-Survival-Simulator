// Written by Sage Mahmud

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [Header("References")]
    Rigidbody2D _rigidbody;
    [SerializeField] Transform _gfx;

    [Header("Attributes")]
    [SerializeField] public float speed = 4f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        // nyooooom
        _rigidbody.velocity = direction * speed * Time.deltaTime;
        Flip();
    }
    
    void Flip()
    {
        // Flip the graphic to face the velocity direction
        Vector2 _newScale = _gfx.localScale;
        if (_rigidbody.velocity.x > 0)
            _newScale.x = -Mathf.Abs(_gfx.localScale.x);
        else if (_rigidbody.velocity.x < 0)
            _newScale.x = Mathf.Abs(_gfx.localScale.x);
        _gfx.localScale = _newScale;
    }
}
