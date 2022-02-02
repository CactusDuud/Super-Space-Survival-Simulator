// Written by Elizabeth Castreje and Sage Mahmud

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Keep track of this object's health
    // maybe when it runs out, destroy it

    [Header("Attributes")]
    [SerializeField] int _maxHealth;
    int _health;

    void Awake()
    {
        _health = _maxHealth;
    }

    void Update()
    {
        if (_health <= 0)
        {
            Debug.Log($"{this.name} has died");
            Destroy(gameObject);
        }
    }
    
    public void Heal(int amount)
    {
        if (_health + amount <= _maxHealth) _health += amount;
        else _health = _maxHealth;
    }

    public void Damage(int amount)
    {
        _health -= amount;
    }
}
