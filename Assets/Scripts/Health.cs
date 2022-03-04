// Written by Elizabeth Castreje, Sage Mahmud, Esmeralda Juarez
/* This script goes in any object that needs a health bar:
 *      takes damage and shows in the UI
 */

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

    [Header("Attributes")]
    public bool doSelfCull;
    [SerializeField] int _maxHealth;
    public int _health;

    //when first awaken the health is at max health
    void Awake()
    {
        _health = _maxHealth;
        SetMaxHealth(_maxHealth);
    }

    //once the health is at 0 or less the gameObject that this script is attatched too is destroyed
    void Update()
    {
        if (doSelfCull && _health <= 0)
        {
            Debug.Log($"{name} has died");
            if(gameObject.CompareTag("EnemyTag")) SpawnManager.GetInstance.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
    }


    //this function sets the health too max health
    public void SetMaxHealth(int setMax)
    {
        slider.maxValue = setMax;
        slider.value = setMax;

        fill.color = healthColor.Evaluate(1f);
    }


    //this function sets health too what is peramenter
    public void SetHealth(int setHealth)
    {
        slider.value = setHealth;

        fill.color = healthColor.Evaluate(slider.normalizedValue);
    }


    //checks if gameobject's health is less than 0
    public bool Alive()
    {
        return _health > 0;
    }
    

    //adds too the health by given peramenter
    public void Heal(int amount)
    {
        if (_health + amount <= _maxHealth) _health += amount;
        else _health = _maxHealth;

        SetHealth(_health);
    }


    //subtracts too health by given perameter (damage taken)
    public void Damage(int amount)
    {
        Debug.Log($"{this.name} lost {amount} health");
        _health -= amount;
        SetHealth(_health);
    }
}