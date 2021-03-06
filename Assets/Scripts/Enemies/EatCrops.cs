// Written by Sage Mahmud

using UnityEngine;

public class EatCrops : MonoBehaviour
{
    [Header("References")]
    Transform _rootPosition;
    [SerializeField] LayerMask _cropLayer;

    [Header("Attributes")]
    [SerializeField] float _eatCooldownDuration = 1.0f;
    [SerializeField] int _damage = 1;
    [SerializeField] float _reach = 1.0f;

    [Header("Statistics")]
    float _eatCooldown;

    void Awake()
    {
        _rootPosition = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        Eat();
        if (_eatCooldown > 0) _eatCooldown -= Time.deltaTime;
    }

    public void Eat()
    {
        if (_eatCooldown <= 0)
        {
            Collider2D hit = Physics2D.OverlapCircle(_rootPosition.position, _reach, _cropLayer);
            
            if (hit != null && hit.CompareTag("CropTag"))
            {
                Debug.Log($"{this.name} nibbled on {hit.name}");
                hit.GetComponent<Health>()?.Damage(_damage);
                _eatCooldown = _eatCooldownDuration;
            }
        }
    }
}
