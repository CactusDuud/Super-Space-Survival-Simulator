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

    [Header("Statistics")]
    float _growthTimer;
    bool _canGrow;
    public bool isHarvestable;

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
        if (_health.Alive())
        {
            if (_canGrow && !isHarvestable)
            {
                if (_growthTimer <= growthDuration) _growthTimer += Time.deltaTime;
                else
                {
                    _spriteRenderer.sprite = _harvestableSprite;
                    isHarvestable = true;
                }
            }
        }
        else
        {
            GameManager.GetInstance.livingCrops -= 1;
            _spriteRenderer.sprite = _deadSprite;
            _canGrow = false;
            isHarvestable = true;
        }
    }

    public void Harvest()
    {
        if (isHarvestable)
        {
            if (_health.Alive())
            {
                _spriteRenderer.sprite = _growingSprite;
                isHarvestable = false;
                GameManager.GetInstance.AddProsperity(_prosperityValue);
                _growthTimer = 0;
            }
            else Destroy(this.gameObject);
        }
    }
}
