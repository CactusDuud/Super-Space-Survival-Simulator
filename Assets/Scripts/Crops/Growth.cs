// Code written by Sage Mahmud & Sebastian Carbajal

using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Health))]
public class Growth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Sprite _growingSprite;
    [SerializeField] Sprite _harvestableSprite;
    [SerializeField] Sprite _deadSprite;
    protected SpriteRenderer _spriteRenderer;
    protected Health _health;

    [Header("Attributes")]
    public float growthDuration;
    [SerializeField] protected int _prosperityValue;
    public int prosperityCost;

    [Header("Statistics")]
    protected float _growthTimer;
    bool _canGrow;
    protected bool _isWithered;
    protected bool _isHarvestable;



    protected virtual void Awake()
    {
        // Get necessary components
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _growingSprite;

        _health= GetComponent<Health>();
    }

    protected virtual void Start()
    {
        // Subscribe to day/night cycle events to enable and disable growth
        GameManager.GetInstance.OnDaytime += EnableGrowth;
        GameManager.GetInstance.OnNighttime += DisableGrowth;
    }

    protected virtual void FixedUpdate()
    {
        // Die if out of health
        if (!_health.IsAlive()) Wither();

        // Grow over time
        GrowClock();
    }

    void EnableGrowth()
    {
        if (_health.IsAlive()) _canGrow = true;
    }

    void DisableGrowth()
    {
        _canGrow = false;
    }

    public bool IsHarvestable()
    {
        return _isHarvestable;
    }

    protected virtual void GrowClock()
    {
        // If still growing, during the daytime, and not yet harvestable...
        if (!_isWithered && _canGrow && !_isHarvestable)
        {
            // increment clock, or otherwise set the crop to be harvestable
            if (_growthTimer <= growthDuration) _growthTimer += Time.deltaTime;
            else
            {
                _spriteRenderer.sprite = _harvestableSprite;
                _isHarvestable = true;
            }
        }
    }

    protected virtual void Wither()
    {
        if (!_isWithered)
        {
            // Remove the crop from the group of living crops, and show that it's dead
            CropManager.GetInstance.RemoveCrop(this.gameObject);
            _spriteRenderer.sprite = _deadSprite;

            // Disable growth, but allow it to be harvested so that it can be removed from the space
            _canGrow = false;
            _isHarvestable = true;
            _isWithered = true;
        }
    }

    public virtual void Harvest()
    {
        // If the crop is ready to harvest...
        if (_isHarvestable)
        {
            // If alive, count it for points and reset it, otherwise just destroy it
            if (_health.IsAlive())
            {
                _spriteRenderer.sprite = _growingSprite;
                _isHarvestable = false;
                GameManager.GetInstance.AddProsperity(_prosperityValue);
                _growthTimer = 0;
            }
            else Destroy(this.gameObject);
        }
    }
}
