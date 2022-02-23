// written by Sage Mahmud, Elizabeth Castreje, Esmeralda Juarez, Miguel Aleman

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(CharacterController))]
[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    Movement _movement;
    CropSpawner _cropSpawner;
    [SerializeField] PlayerTool _currentTool;

    [Header("Statistics")]
    [SerializeField] Vector2 _movementDirection;


    void Awake()
    {
        // Fetch components
        _cropSpawner = transform.GetChild(2).GetComponent<CropSpawner>();
        _movement = GetComponent<Movement>();
    }

    // Functions to be called by InputAction events
    // I know it's awful but I'm not sure how to do it otherwise :(
    public void OnMove(InputAction.CallbackContext ctx) { _movementDirection = ctx.ReadValue<Vector2>(); }
    public void OnUseTool(InputAction.CallbackContext ctx) { _currentTool.UseTool(); }
    public void OnPauseMenu(InputAction.CallbackContext ctx) { PauseMenu.GetInstance.TogglePause(); }
    public void OnPlantCrop1(InputAction.CallbackContext ctx) 
    {
        //InputAction.CallbackContext has three phases
        //  start
        //  performed
        //  cancelled
        //so we are just calling CropSpawner functions in started phase
        if (ctx.started == true)
        {
            _cropSpawner.CreatePlant(CropSpawner.plantType.potato);
        }
    }
    public void OnPlantCrop2(InputAction.CallbackContext ctx) 
    {
        if (ctx.started == true)
        {
            _cropSpawner.CreatePlant(CropSpawner.plantType.carrot);
        }
    }
    public void OnPlantCrop3(InputAction.CallbackContext ctx) 
    {
        if (ctx.started == true)
        {
            _cropSpawner.CreatePlant(CropSpawner.plantType.beet);
        }
    }
    public void OnPlantCrop4(InputAction.CallbackContext ctx) 
    {
        if (ctx.started == true)
        {
            _cropSpawner.CreatePlant(CropSpawner.plantType.sunflower);
        }
    }
    public void OnPlantCrop5(InputAction.CallbackContext ctx) 
    {
        if (ctx.started == true)
        {
            _cropSpawner.CreatePlant(CropSpawner.plantType.moonflower);
        }
    }

    void FixedUpdate()
    {
        if (!_currentTool.IsUsingTool()) _movement.Move(_movementDirection);
        else _movement.Move(Vector2.zero);
    }
}
