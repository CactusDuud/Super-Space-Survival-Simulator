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
        // TODO: Replace with ai eventually
        Vector2 targetDirection = DetermineDirection();
        _movement.Move(targetDirection);
    }

    protected virtual Vector2 DetermineDirection()
    {
        return (_movePoint.position - transform.position).normalized;
    }

    protected void FindMovePoint()
    {
        GameObject[] _cropTargets = GameObject.FindGameObjectsWithTag("CropTag");

        if (_cropTargets.Length != 0)
        {
            GameObject _potentialTarget = _cropTargets[Random.Range(0, _cropTargets.Length - 1)];
            if (_potentialTarget.GetComponent<Health>()?.Alive() == true)
                _movePoint = _potentialTarget.GetComponent<Transform>();
            else
                _movePoint = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<Transform>();
        }
        else
        {
            _movePoint = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<Transform>();
        }
    }
}
