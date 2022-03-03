using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBack : Growth
{
    [SerializeField] private int attackDamage;
    private Health _currentHealth;
    private int _prevHealth;
    private bool _isAttacked;

    private GameObject _enemy;


    //on awake get the current health and then prevhealth too see if damage is being taken

    protected override void Awake()
    {
        base.Awake();
        _isAttacked = false;
        _currentHealth = GetComponent<Health>();
        _prevHealth = _currentHealth._health;
        _enemy = null;
    }

    //override fixedupdate too check if the health is going down
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (_currentHealth._health < _prevHealth) //being attacked
        {
            _isAttacked = true;
            _prevHealth = _currentHealth._health;
        }
        else if (_currentHealth._health == _prevHealth) // not being attacked
        {
            _isAttacked = false;
        }
        else if (_currentHealth._health > _prevHealth)
        {
            _isAttacked = false;
            _prevHealth = _currentHealth._health;
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("EnemyTag") && _isAttacked && _enemy == null)
        {
            Debug.Log("fight back plant being attacked");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyTag") && _isAttacked && _enemy == null)
        {
            Debug.Log("fight back plant being attacked");
        }
    }

}
