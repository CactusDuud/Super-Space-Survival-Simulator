using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakyAI : TargetingAI
{
    [Header("Attributes")]
    [SerializeField] float _walkDuration = 4;
    [SerializeField] float _hideDuration = 3;

    [Header("Statistics")]
    float _hideTimer;
    bool isHiding = false;

    protected override void Awake()
    {
        base.Awake();

        _hideTimer = _walkDuration;
    }

    protected override void FixedUpdate()
    {
        HideClock();

        base.FixedUpdate();
    }

    protected override Vector2 DetermineDirection()
    {
        if (isHiding)
        {
            return Vector2.zero;
        }
        else
        {
            return (_movePoint.position - transform.position).normalized;
        }
    }

    void HideClock()
    {
        _hideTimer -= Time.deltaTime;
        if (_hideTimer <= 0)
        {
            if (isHiding)
            {
                _hideTimer = _walkDuration;
                isHiding = false;
            }
            else
            {
                _hideTimer = _hideDuration;
                isHiding = true;
            }
        }
        
    }
}
