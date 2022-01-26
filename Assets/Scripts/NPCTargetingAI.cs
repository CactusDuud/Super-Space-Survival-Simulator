// Written by Elizabeth Castreje and Sage Mahmud

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class NPCTargetingAI : MonoBehaviour
{
    // This class should determine a location for the object to move
    // and feed a vector to that point that into a movement script

    [Header("References")]
    Movement _movement;
    [SerializeField] Transform _movePoint;

    void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    void FixedUpdate()
    {
        // TODO: Replace with ai eventually
        Vector2 targetDirection = _movePoint.position - transform.position;
        targetDirection = targetDirection.normalized;

        _movement.Move(targetDirection);
    }
}
