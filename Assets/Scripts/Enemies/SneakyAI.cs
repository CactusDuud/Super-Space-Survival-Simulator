// Written by Sage Mahmud

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
    bool _isHiding;

    protected override void Awake()
    {
        base.Awake();

        _hideTimer = _walkDuration;
    }

    protected override void FixedUpdate()
    {
        HideBehaviourClock();

        base.FixedUpdate();
    }

    protected override Vector2 DetermineDirection()
    {
        if (_movePoint != null)
        {
            if (_isHiding)
            {
                return Vector2.zero;
            }
            else
            {
                return (_movePoint.position - transform.position).normalized;
            }
        }
        else return Vector2.zero;
    }

    void HideBehaviourClock()
    {
        _hideTimer -= Time.deltaTime;
        if (_hideTimer <= 0)
        {
            if (_isHiding)
            {
                _hideTimer = _walkDuration;
                _isHiding = false;
            }
            else
            {
                _hideTimer = _hideDuration;
                _isHiding = true;
            }
        }
    }
}
