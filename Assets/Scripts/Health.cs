// Written by Elizabeth Castreje and Sage Mahmud

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // Keep track of this object's health
    // maybe when it runs out, destroy it

    [Header("UI effects")]
    public Slider slider;
    public Gradient healthColor;
    public Image fill;
    //public GameObject healthObject;

    [Header("Attributes")]
    public bool doSelfCull;
    [SerializeField] int _maxHealth;
    int _health;

    void Awake()
    {
        _health = _maxHealth;
        SetMaxHealth(_maxHealth);
    }

    void Update()
    {
        if (doSelfCull && _health <= 0)
        {
            //Debug.Log($"{healthObject.name} has died");
            //Destroy(healthObject);
            //Debug.Log($"{this.name} has died");
            //Destroy(gameObject);
        }
    }


    public void SetMaxHealth(int setMax)
    {
        slider.maxValue = setMax;
        slider.value = setMax;

        fill.color = healthColor.Evaluate(1f);
    }

    public void SetHealth(int setHealth)
    {
        slider.value = setHealth;

        fill.color = healthColor.Evaluate(slider.normalizedValue);
    }

    public bool Alive()
    {
        return _health > 0;
    }
    
    public void Heal(int amount)
    {
        if (_health + amount <= _maxHealth) _health += amount;
        else _health = _maxHealth;

        SetHealth(_health);
    }

    public void Damage(int amount)
    {
        _health -= amount;
        SetHealth(_health);
    }
}