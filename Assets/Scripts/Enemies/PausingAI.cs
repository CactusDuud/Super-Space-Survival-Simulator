// Written by Sage Mahmud


using UnityEngine;

public class PausingAI : TargetingAI
{
    //TODO: Change image when stopped
    [Header("Attributes")]
    [SerializeField] float _walkDuration = 4f;
    [SerializeField] float _hideDuration = 3f;

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
        if (_isHiding) return Vector2.zero;
        else return (Vector3)_path.vectorPath[_currentWaypoint] - transform.position;
    }

    void HideBehaviourClock()
    {
        if (_hideTimer > 0) _hideTimer -= Time.deltaTime;
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
