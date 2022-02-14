// Written by Elizabeth Castreje and Sage Mahmud

using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Movement))]
public class TargetingAI : MonoBehaviour
{
    // This class should determine a location for the object to move
    // and feed a vector to that point that into a movement script

    [Header("References")]
    protected Movement _movement;
    [SerializeField] protected Transform _target;

    [Header("Pathfinding")]
    protected Path _path;
    Seeker _seeker;
    protected int _currentWaypoint;
    float _nextWaypointDistance = 0.1f;
    bool _reachedTarget;
    float _pathUpdateRate = 0.5f;

    protected virtual void Awake()
    {
        _movement = GetComponent<Movement>();
        _seeker= GetComponent<Seeker>();
    }

    void Start()
    {
        FindTarget();
        InvokeRepeating("UpdatePath", 0f, _pathUpdateRate);
    }

    protected virtual void FixedUpdate()
    {
        if (_path != null)
        {
            // Check if we are already where we want to be
            if (_currentWaypoint >= _path.vectorPath.Count) _reachedTarget = true;
            else _reachedTarget = false;

            // Move towards our target otherwise
            _movement.Move(DetermineDirection().normalized);

            // Check if we've successfully stepped towards the target
            if (Vector2.Distance(transform.position, _path.vectorPath[_currentWaypoint]) <= _nextWaypointDistance) _currentWaypoint++;
        }
    }

    protected virtual Vector2 DetermineDirection()
    {
        return (Vector3)_path.vectorPath[_currentWaypoint] - transform.position;
    }

    void UpdatePath()
    {
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

    protected void FindTarget()
    {
        // Null case (find a new target)
        if (_target == null)
        {
            _target = CropManager.GetInstance.GetRandomCrop().transform;

            if (_target == null)  _target = GameObject.FindGameObjectWithTag("PlayerTag").transform;
        }
        // Non-null case (check if the target is dead; if so, look for a new one)
        else if (_target.CompareTag("CropTag"))
        {
            if (!_target.GetComponent<Health>()?.Alive() ?? false)
            {
                Debug.Log($"{_target.name} is dead");
                _target = null;
            } 
        }
        
    }
}
