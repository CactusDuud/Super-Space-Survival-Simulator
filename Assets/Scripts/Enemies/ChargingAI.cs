// Written by Sage Mahmud


using UnityEngine;

public class ChargingAI : TargetingAI
{
    [Header("References")]
    Transform _chargeTarget;

    [Header("Attributes")]
    [SerializeField] float _chargeDistance = 25f;
    [SerializeField] float _chargeSpeedFactor = 1.5f;
    [SerializeField] float _chargeDuration = 1f;
    [SerializeField] float _chargeCooldownDuration = 5f;

    [Header("Statistics")]
    [SerializeField] float _chargeTime;
    [SerializeField] float _chargeCooldownTimer;

    protected override void Awake()
    {
        base.Awake();

        _chargeTarget = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<Transform>();
    }

    protected override void FixedUpdate()
    {
        if (_chargeTime <= 0 && _chargeCooldownTimer <= 0 && FindEnemyDistance() <= _chargeDistance) _chargeTime = _chargeDuration;
        ChargeBehaviourClock();
        if (_chargeTime <= 0 && _chargeCooldownTimer <= 0) _chargeCooldownTimer = _chargeCooldownDuration;
        ChargeCooldownClock();

        base.FixedUpdate();
    }

    protected override Vector2 DetermineDirection()
    {
        if (_target != transform)
        {
            if (_chargeTime > 0)
            {
                _movement.speed = _baseSpeed * _chargeSpeedFactor;
                return _chargeTarget.position - transform.position;
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

    void ChargeBehaviourClock()
    {
        if (_chargeTime > 0) _chargeTime -= Time.deltaTime;
    }

    void ChargeCooldownClock()
    {
        if (_chargeCooldownTimer > 0) _chargeCooldownTimer -= Time.deltaTime;
    }

    protected override void DaySlow()
    {
        if (_chargeTime > 0) _movement.speed = _baseSpeed * _chargeSpeedFactor * _daySpeedPenalty;
        else _movement.speed = _baseSpeed * _daySpeedPenalty;
    }

    protected override void NightUnslow()
    {
        if (_chargeTime > 0) _movement.speed = _baseSpeed * _chargeSpeedFactor;
        else _movement.speed = _baseSpeed;
    }
}
