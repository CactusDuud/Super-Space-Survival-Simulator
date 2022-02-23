//Code written by Sage Mahmud & Sebastian Carbajal

using System.Collections;
using System.Collections.Generic;
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
    bool _isWithered;
    bool _isHarvestable;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _growingSprite;

        _health= GetComponent<Health>();
    }

    void Start()
    {
        GameManager.GetInstance.OnDaytime += EnableGrowth;
        GameManager.GetInstance.OnNighttime += DisableGrowth;
    }

    void FixedUpdate()
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

    void GrowClock()
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

    void Wither()
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

    public void Harvest()
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
