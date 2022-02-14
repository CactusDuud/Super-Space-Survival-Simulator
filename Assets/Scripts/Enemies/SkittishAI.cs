// Written by Sage Mahmud

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkittishAI : TargetingAI
{
    [Header("References")]
    Transform _fleeTarget;

    [Header("Attributes")]
    [SerializeField] float _fleeDistance = 5f;
    [SerializeField] float _fleeSpeedFactor = 1.5f;
    [SerializeField] float _fleeDuration = 1f;

    [Header("Statistics")]
    float _fleeTimer;

    protected override void Awake()
    {
        base.Awake();

        _fleeTarget = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<Transform>();
    }

    protected override void FixedUpdate()
    {
        if (_fleeTimer <= 0 && FindEnemyDistance() <= _fleeDistance) _fleeTimer = _fleeDuration;
        FleeBehaviourClock();

        base.FixedUpdate();
    }

    protected override Vector2 DetermineDirection()
    {
        if (_target != null)
        {
            if (_fleeTimer > 0)
            {
                _movement.speed = _baseSpeed * _fleeSpeedFactor;
                return transform.position - _fleeTarget.position;
            }
            else
            {
                _movement.speed = _baseSpeed;
                return (Vector3)_path.vectorPath[_currentWaypoint] - transform.position;
            }
        }
        else return Vector2.zero;
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

    protected override void DaySlow()
    {
        if (_fleeTimer > 0) _movement.speed = _baseSpeed * _fleeSpeedFactor * _daySpeedPenalty;
        else _movement.speed = _baseSpeed * _daySpeedPenalty;
    }

    protected override void NightUnslow()
    {
        if (_fleeTimer > 0) _movement.speed = _baseSpeed * _fleeSpeedFactor;
        else _movement.speed = _baseSpeed;
    }
}
