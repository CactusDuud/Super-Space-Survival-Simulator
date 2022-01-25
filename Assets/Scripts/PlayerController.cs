using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Super Important Top-of-The-List Variables")]
    [SerializeField] Rigidbody2D _rigidbody;
    PlayerControls _controls;

    // Closed-access information variables about the player
    [Header("Player Statistics")]
    Vector2 _movement_direction;

    // Public player stats and abilities
    [Header("Player Attributes")]
    public float player_speed = 4f;

    
    void Awake()
    {
        _rigidbody = GetComponentInParent<Rigidbody2D>();
        
        // Creates a new instance of player _controls
        _controls = new PlayerControls();
        
        // Subscribe to Left Joystick and WASD movement events
        _controls.Gameplay.Movement.performed += ctx => _movement_direction = ctx.ReadValue<Vector2>();
        _controls.Gameplay.Movement.canceled += ctx => _movement_direction = Vector2.zero;
    }

    void OnEnable()
    {
        // Turns on player controller when the player is enabled
        _controls.Enable();
    }

    void OnDisable()
    {
        // Turns off player controller when the player is enabled
        _controls.Disable();
    }

    // Every step!
    void FixedUpdate()
    {
        _rigidbody.velocity = _movement_direction * player_speed;
    }
}
