using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerTool : MonoBehaviour
{
    [Header("References")]
    Collider2D _effectArea;
    SpriteRenderer _effectSprite;

    [Header("Attributes")]
    [SerializeField] float _useDuration = 0.5f;
    [SerializeField] int _damage = 1;

    [Header("Statistics")]
    float _useTime;

    void Awake()
    {
        _effectArea = GetComponent<Collider2D>();
        _effectSprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (_effectArea.enabled)
        {
            _useTime -= Time.deltaTime;

            if (_useTime <= 0)
            {
                _effectArea.enabled = false;
                _effectSprite.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<Health>()?.Damage(_damage);
    }

    public void UseTool()
    {
        if (_effectArea.enabled == false)
        {
            _useTime = _useDuration;
            _effectArea.enabled = true;
            _effectSprite.enabled = true;
        }
    }
}
