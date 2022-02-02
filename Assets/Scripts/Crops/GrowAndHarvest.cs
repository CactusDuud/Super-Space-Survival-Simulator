using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GrowAndHarvest : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Sprite _growingSprite;
    [SerializeField] Sprite _harvestableSprite;
    SpriteRenderer _spriteRenderer;

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
        _canGrow = true;
    }

    void DisableGrowth()
    {
        _canGrow = false;
    }

    void GrowClock()
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

    public void Harvest()
    {
        if (isHarvestable)
        {
            _spriteRenderer.sprite = _growingSprite;
            isHarvestable = false;
            GameManager.GetInstance.AddProsperity(_prosperityValue);
            _growthTimer = 0;
        }
    }
}
