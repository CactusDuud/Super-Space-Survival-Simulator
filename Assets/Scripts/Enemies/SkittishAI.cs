// Written by Sage Mahmud

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkittishAI : TargetingAI
{
    [Header("References")]
    GameObject _fleeTarget;
    float _baseSpeed;

    [Header("Attributes")]
    [SerializeField] float _fleeDistance = 5f;
    [SerializeField] float _fleeSpeedFactor = 1.5f;
    [SerializeField] float _fleeDuration = 1f;

    [Header("Statistics")]
    float _fleeTimer;

    protected override void Awake()
    {
        base.Awake();

        _baseSpeed = _movement.speed;
    }

    protected override void FixedUpdate()
    {
        if (_fleeTimer <= 0 && FindEnemyDistance() <= _fleeDistance) _fleeTimer = _fleeDuration;
        FleeBehaviourClock();

        base.FixedUpdate();
    }

    protected override Vector2 DetermineDirection()
    {
        if (_fleeTimer > 0)
        {
            _movement.speed = _baseSpeed * _fleeSpeedFactor;
            return (transform.position - _movePoint.position).normalized;
        }
        else
        {
            _movement.speed = _baseSpeed;
            return (_movePoint.position - transform.position).normalized;
        }
    }

    float FindEnemyDistance()
    {
        GameObject[] _players;
        _players = GameObject.FindGameObjectsWithTag("PlayerTag");

        float _distance = Mathf.Infinity;
        foreach (GameObject _player in _players)
        {
            float _currentDistance = (_player.transform.position - transform.position).sqrMagnitude;
            if (_currentDistance < _distance)
            {
                _distance = _currentDistance;
            }
        }

        return _distance;
    }

    void FleeBehaviourClock()
    {
        if (_fleeTimer > 0) _fleeTimer -= Time.deltaTime;
    }
}
