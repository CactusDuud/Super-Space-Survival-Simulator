//Code written by Sage Mahmud & Sebastian Carbajal

using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Health))]
public class Growth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Sprite _growingSprite;
    [SerializeField] Sprite _harvestableSprite;
    [SerializeField] Sprite _deadSprite;
    SpriteRenderer _spriteRenderer;
    Health _health;

    [Header("Attributes")]
    public float growthDuration;
    [SerializeField] int _prosperityValue;
    public int prosperityCost;

    [Header("Statistics")]
    float _growthTimer;
    bool _canGrow;
    protected bool _isWithered;
    bool _isHarvestable;

    protected virtual void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _growingSprite;

        _health= GetComponent<Health>();
    }

    protected virtual void Start()
    {
        GameManager.GetInstance.OnDaytime += EnableGrowth;
        GameManager.GetInstance.OnNighttime += DisableGrowth;
    }

    protected virtual void FixedUpdate()
    {
        if (!_health.Alive()) Wither();
        GrowClock();
    }

    void EnableGrowth()
    {
        if (_health.Alive()) _canGrow = true;
    }

    void DisableGrowth()
    {
        _canGrow = false;
    }

    protected virtual void GrowClock()
    {
        if (!_isWithered)
        {
            if (_canGrow && !_isHarvestable)
            {
                if (_growthTimer <= growthDuration) _growthTimer += Time.deltaTime;
                else
                {
                    _spriteRenderer.sprite = _harvestableSprite;
                    _isHarvestable = true;
                }
            } 
        }
    }

    protected virtual void Wither()
    {
        if (!_isWithered)
        {
            CropManager.GetInstance.RemoveCrop(this.gameObject);
            _spriteRenderer.sprite = _deadSprite;
            _canGrow = false;
            _isHarvestable = true;
            _isWithered = true;
        }
    }

    public virtual void Harvest()
    {
        if (_isHarvestable)
        {
            if (_health.Alive())
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
