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

        // Save some filler variables for later reference
        _baseSpeed = _movement.speed;
        _target = transform;
    }

    void Start()
    {
        // Subscribe to day/night to slow down enemies
        GameManager.GetInstance.OnDaytime += DaySlow;
        GameManager.GetInstance.OnNighttime += NightUnslow;

        // Find the first target (just in case)
        FindTarget();

        // Repeatedly update the AI path to point towards the target starting now
        InvokeRepeating("UpdatePath", 0f, _pathUpdateRate);
    }

    protected virtual void FixedUpdate()
    {
        // If there is currently a path...
        if (_path != null)
        {
            // Check if we are already where we want to be
            if (_currentWaypoint < _path.vectorPath.Count)
            {
                // If the target exists
                if (_target != null)
                {
                    // Move towards our target
                    if (Vector2.Distance(_target.position, transform.position) > _stoppingDistance)
                        _movement.Move(DetermineDirection().normalized);
                    else
                        _movement.Move(DetermineDirection());
                }
                // Otherwise set to a "zero" value (where the AI is)
                else
                {
                    _target = transform;
                }

                // Check if we've successfully stepped towards the target
                if (Vector2.Distance(transform.position, _path.vectorPath[_currentWaypoint]) <= _nextWaypointDistance)
                    _currentWaypoint++;
            }    
        }
    }

    protected virtual Vector2 DetermineDirection()
    {
        // Determine the next vector to move in
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
        // Imma be honest this is just a failsafe dw about it
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

            // Backup if there are no crops (target the player)
            if (_target == null)  _target = GameObject.FindGameObjectWithTag("PlayerTag").transform;
        }
        // Non-null case (check if the target is dead; if so, look for a new one)
        else if (_target.CompareTag("CropTag"))
        {
            if (!_target.GetComponent<Health>()?.IsAlive() ?? false) _target = transform;
        }
        
    }

    protected virtual void DaySlow()
    {
        // Set speed to a slower one if it's daytime
        _movement.speed = _baseSpeed * _daySpeedPenalty;
    }

    protected virtual void NightUnslow()
    {
        // Reset the speed at night
        _movement.speed = _baseSpeed;
    }
}
