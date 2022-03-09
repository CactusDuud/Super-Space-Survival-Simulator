// Written by Sage Mahmud, Elizabeth Castreje, and Miguel Aleman

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerTool : MonoBehaviour
{
    [Header("References")]
    Collider2D _effectArea;
    SpriteRenderer _effectSprite;

    [Header("Attributes")]
    [SerializeField] float _useDuration = 0.5f;
    [SerializeField] int _damage = 1;

    [Header("Statistics")]
    bool _isUsingTool;
    float _useTime;

    void Awake()
    {
        _effectArea = GetComponent<Collider2D>();
        _effectSprite = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // Enable the tool for a few frames if the player has pressed the right key
        if (_isUsingTool)
        {
            _useTime -= Time.deltaTime;

            if (_useTime <= 0.01)
            {
                _isUsingTool = false;
                _effectArea.enabled = false;
                _effectSprite.enabled = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Damage/harvest hit objects
        if (other.CompareTag("EnemyTag")) other.GetComponent<Health>()?.Damage(_damage);
        else if (other.CompareTag("CropTag"))
        {
            other.GetComponent<Growth>()?.Harvest();
            other.GetComponent<CropDeath>()?.Harvest();
        }
    }

    public void UseTool()
    {
        // Only use the tool if the game is playing and the button is pressed
        if (!PauseMenu.GamePaused && !_isUsingTool)
        {
            _isUsingTool = true;
            _useTime = _useDuration;
            _effectArea.enabled = true;
            _effectSprite.enabled = true;
        }

    }

    public bool IsUsingTool()
    {
        return _isUsingTool;
    }
}
