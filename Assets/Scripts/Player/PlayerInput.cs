// written by Sage Mahmud, Elizabeth Castreje, Esmeralda Juarez, Miguel Aleman


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(CropSpawner))]
public class PlayerInput : MonoBehaviour
{
    [Header("References")]
    PlayerControls _controls;
    Movement _movement;
    CropSpawner _cropSpawner;
    [SerializeField] PlayerTool _currentTool;

    [Header("Statistics")]
    [SerializeField] Vector2 _movementDirection;


    void Awake()
    {   
        // Creates a new instance of player _controls
        _controls = new PlayerControls();

        // Fetch components
        _cropSpawner = GetComponent<CropSpawner>();
        
        // Subscribe to Left Joystick and WASD movement events
        _controls.Gameplay.Movement.performed += ctx => _movementDirection = ctx.ReadValue<Vector2>();
        _controls.Gameplay.Movement.canceled += ctx => _movementDirection = Vector2.zero;

        // Subscribe to keyboard q press event to use the current tool
        _controls.Gameplay.UseTool.performed += _ => _currentTool.UseTool();

        //Each one Subscribes to a PlantCrop
        _controls.Gameplay.PlantCrop1.performed += ctx => _cropSpawner.CreatePlant("potato");//plant crop 1 function
        //_controls.Gameplay.PlantCrop2.performed += ctx => _cropSpawner.CreatePlant("potato");//plant crop 2 function
        //_controls.Gameplay.PlantCrop3.performed += ctx => _cropSpawner.CreatePlant("potato");//plant crop 3 function 
        //_controls.Gameplay.PlantCrop4.performed += ctx => _cropSpawner.CreatePlant("potato");//plant crop 4 function 
        //_controls.Gameplay.PlantCrop5.performed += ctx => _cropSpawner.CreatePlant("potato");//plant crop 5 function 

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
