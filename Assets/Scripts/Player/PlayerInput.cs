using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
public class PlayerInput : MonoBehaviour
{
    [Header("References")]
    PlayerControls _controls;
    Movement _movement;

    [Header("Statistics")]
    [SerializeField] Vector2 _movementDirection;
   
    void Awake()
    {   
        // Creates a new instance of player _controls
        _controls = new PlayerControls();
        
        // Subscribe to Left Joystick and WASD movement events
        _controls.Gameplay.Movement.performed += ctx => _movementDirection = ctx.ReadValue<Vector2>();
        _controls.Gameplay.Movement.canceled += ctx => _movementDirection = Vector2.zero;

        _movement = GetComponent<Movement>();
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

    void FixedUpdate()
    {
        _movement.Move(_movementDirection);
    }

    // Not sure if this is necessary
    // void OnDestroy()
    // {
    //     // Unsubscribe to Left Joystick and WASD movement events
    //     _controls.Gameplay.Movement.performed -= ctx => _movementDirection = ctx.ReadValue<Vector2>();
    //     _controls.Gameplay.Movement.canceled -= ctx => _movementDirection = Vector2.zero;
    // }
    
}
