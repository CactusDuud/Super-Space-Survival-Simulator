// Written by Sage Mahmud

using UnityEngine;

public class DashingAI : TargetingAI
{
    [Header("Attributes")]
    [SerializeField] float _dashDuration = 0.5f;
    [SerializeField] float _pauseDuration = 3f;

    [Header("Statistics")]
    float _pauseTimer;
    bool _isPausing;

    protected override void Awake()
    {
        base.Awake();

        _pauseTimer = _dashDuration;
    }

    protected override void FixedUpdate()
    {
        PauseBehaviourClock();

        base.FixedUpdate();
    }

    // Determine the direction to move in
    protected override Vector2 DetermineDirection()
    {
        if (_isPausing) return Vector2.zero;
        else return (Vector3)_path.vectorPath[_currentWaypoint] - transform.position;
    }

    // The clock to measure how long this AI should be standing still for (between dashes)
    void PauseBehaviourClock()
    {
        if (_pauseTimer > 0) _pauseTimer -= Time.deltaTime;
        if (_pauseTimer <= 0)
        {
            if (_isPausing)
            {
                _pauseTimer = _dashDuration;
                _isPausing = false;
            }
            else
            {
                _pauseTimer = _pauseDuration;
                _isPausing = true;
            }
        }
    }
}
