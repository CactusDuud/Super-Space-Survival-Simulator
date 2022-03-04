// Written by Elizabeth Castreje and Sage Mahmud

using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(Movement))]
public class TargetingAI : MonoBehaviour
{
    // This class should determine a location for the object to move
    // and feed a vector to that point that into a movement script

    [Header("References")]
    protected Movement _movement;
    [SerializeField] protected Transform _target;
    Seeker _seeker;
    protected float _baseSpeed;

    [Header("Attributes")]
    [SerializeField] protected float _daySpeedPenalty = 0.5f;

    [Header("Pathfinding")]
    protected Path _path;
    protected int _currentWaypoint;
    [SerializeField] float _nextWaypointDistance = 0.1f;
    [SerializeField] float _pathUpdateRate = 0.5f;
    [SerializeField] protected float _stoppingDistance = 1.0f;

    protected virtual void Awake()
    {
        _movement = GetComponent<Movement>();
        _seeker= GetComponent<Seeker>();

        _baseSpeed = _movement.speed;
        _target = transform;
    }

    void Start()
    {
        GameManager.GetInstance.OnDaytime += DaySlow;
        GameManager.GetInstance.OnNighttime += NightUnslow;

        FindTarget();
        InvokeRepeating("UpdatePath", 0f, _pathUpdateRate);
    }

    protected virtual void FixedUpdate()
    {
        if (_path != null)
        {
            // Check if we are already where we want to be
            if (_currentWaypoint < _path.vectorPath.Count)
            {
                // Move towards our target if it's farther than the stopping distance
                if (Vector2.Distance(_target.position, transform.position) > _stoppingDistance)
                    _movement.Move(DetermineDirection().normalized);
                else
                    _movement.Move(DetermineDirection());

                // Check if we've successfully stepped towards the target
                if (Vector2.Distance(transform.position, _path.vectorPath[_currentWaypoint]) <= _nextWaypointDistance)
                    _currentWaypoint++;
            }    
        }
    }

    protected virtual Vector2 DetermineDirection()
    {
        return (Vector3)_path.vectorPath[_currentWaypoint] - transform.position;
    }

    void UpdatePath()
    {
        // Only look for a new path if not calculating one right now
        if (_seeker.IsDone())
        {
            FindTarget();
            _seeker.StartPath(transform.position, _target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
        }
    }

    protected virtual void FindTarget()
    {
        // Null case (find a new target)
        if (_target == transform)
        {
            _target = CropManager.GetInstance.GetRandomCrop().transform;

            if (_target == transform)  _target = GameObject.FindGameObjectWithTag("PlayerTag").transform;
        }
        // Non-null case (check if the target is dead; if so, look for a new one)
        else if (_target.CompareTag("CropTag"))
        {
            if (!_target.GetComponent<Health>()?.Alive() ?? false) _target = transform;
        }
        
    }

    protected virtual void DaySlow()
    {
        _movement.speed = _baseSpeed * _daySpeedPenalty;
    }

    protected virtual void NightUnslow()
    {
        _movement.speed = _baseSpeed;
    }
}
