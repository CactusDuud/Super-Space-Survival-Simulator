// Written by Sage Mahmud

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
    bool _isUsingTool;
    float _useTime;

    void Awake()
    {
        _effectArea = GetComponent<Collider2D>();
        _effectSprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (_isUsingTool)
        {
            _useTime -= Time.deltaTime;

            if (_useTime <= 0)
            {
                _isUsingTool = false;
                _effectArea.enabled = false;
                _effectSprite.enabled = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyTag")) other.GetComponent<Health>()?.Damage(_damage);
        else if (other.CompareTag("CropTag")) other.GetComponent<Growth>()?.Harvest();
    }

    public void UseTool()
    {
        if (_isUsingTool == false)
        {
            _isUsingTool = true;
            _useTime = _useDuration;
            _effectArea.enabled = true;
            _effectSprite.enabled = true;
        }
    }

    public bool IsUsingTool()
    {
        return _isUsingTool;
    }
}
