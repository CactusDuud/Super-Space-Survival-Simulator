// Written by Elizabeth Castreje and Sage Mahmud

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Keep track of this object's health
    // maybe when it runs out, destroy it

    [Header("Attributes")]
    [SerializeField] int _health;

    // void OnDestroy()
    // {
        
    // }
    
    void AddHealth(int amount)
    {
        _health += amount;
    }
}
