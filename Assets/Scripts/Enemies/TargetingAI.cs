// Written by Elizabeth Castreje and Sage Mahmud

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class TargetingAI : MonoBehaviour
{
    // This class should determine a location for the object to move
    // and feed a vector to that point that into a movement script

    [Header("References")]
    protected Movement _movement;
    [SerializeField] protected Transform _movePoint;

    protected virtual void Awake()
    {
        _movement = GetComponent<Movement>();
        FindMovePoint();
    }

    protected virtual void FixedUpdate()
    {
        // TODO: Replace with navmesh or A* AI eventually
        Vector2 targetDirection = DetermineDirection();
        _movement.Move(targetDirection.normalized);
    }

    protected virtual Vector2 DetermineDirection()
    {
        if (_movePoint != null) return (_movePoint.position - transform.position);
        else return Vector2.zero;
    }

    protected void FindMovePoint()
    {
        if (_movePoint == null)
        {
            GameObject[] _possibleTargets = GameObject.FindGameObjectsWithTag("CropTag");

            if (_possibleTargets.Length != 0)
            {
                GameObject _potentialTarget = _possibleTargets[Random.Range(0, _possibleTargets.Length - 1)];
                Health healthScript = _potentialTarget.GetComponent<Health>();
                if (healthScript != null && healthScript.Alive())
                    _movePoint = _potentialTarget.GetComponent<Transform>();
                else
                    _movePoint = null;
            }
            else
            {
                _movePoint = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<Transform>();
            }
        }
        else if (_movePoint.CompareTag("CropTag"))
        {
            Health healthScript = _movePoint.GetComponent<Health>();
            if (healthScript != null && !healthScript.Alive()) _movePoint = null;
        }
        
    }
}
