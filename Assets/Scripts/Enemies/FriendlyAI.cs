// Written by Sage Mahmud

using UnityEngine;

public class FriendlyAI : TargetingAI
{
    [Header("References")]
    [SerializeField] Transform _wanderPoint;

    [Header("Attributes")]
    [SerializeField] float _sightRange = 2.5f;
    [SerializeField] float _wanderRange = 6f;



    protected override void Awake()
    {
        base.Awake();
        _wanderPoint.SetParent(null, true);
    }

    protected override void FindTarget()
    {
        // Null case
        if (_target == transform || _target == null)
        {
            // If there are living enemies, pick a random one and move towards it if it's close enough
            if (SpawnManager.GetInstance.IsLivingEnemies())
            {
                Transform randomEnemy = SpawnManager.GetInstance.GetRandomEnemy().transform;
                if (Vector2.Distance(transform.position, _target.position) <= _sightRange) _target = randomEnemy;
            }

            // If the target is still null (meaning none was found), just wander
            if (_target == transform || _target == null)
            {
                _wanderPoint.position = transform.position;
                Vector3 randomOffset = new Vector3(
                    Random.Range(-_wanderRange, _wanderRange),
                    Random.Range(-_wanderRange, _wanderRange),
                    0
                );
                _wanderPoint.position = randomOffset;

                _target = _wanderPoint;
            }
        }
        // Non-null case (check if the target is dead; if so, look for a new one)
        else if (_target.CompareTag("EnemyTag"))
        {
            if (!_target.GetComponent<Health>()?.IsAlive() ?? false) _target = transform;
        }
        else if (_target == _wanderPoint && Vector2.Distance(transform.position, _target.position) < _stoppingDistance) _target = transform;
    }
}
