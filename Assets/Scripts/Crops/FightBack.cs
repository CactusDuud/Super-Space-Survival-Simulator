//Contributor: Esmeralda Juarez

/*
 * this was made as a child but now is just a component
 * this script is a child from Growth script class for the plants
 *      with this script attached to a plant when plant is taking damage from an enemy it fights back
 *      this child only focuses on one enemy until they die then moves on too the next attacker
 *      or attackes until the plant dies (reaches 0 health)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightBack : MonoBehaviour
{
    [SerializeField] private int attackDamage;
    private Health _currentHealth;
    private int _prevHealth;
    private bool _isAttacked;
    private bool _isCooling;

    private GameObject _enemy;


    //on awake initiate variables
    void Awake()
    {
        _isAttacked = false;
        _isCooling = false;
        _currentHealth = GetComponent<Health>();
        _prevHealth = _currentHealth._health;
        _enemy = null;
    }

    //override fixedupdate too check if the health is going down or not
    void FixedUpdate()
    {

        // if plant has 0 health it no longer fights back
        if (_currentHealth._health > 0) //&& _isHarvestable)
        {
            if (_currentHealth._health < _prevHealth) //being attacked so calling function too fight back
            {
                _isAttacked = true;
                StartCoroutine(updateHealth(1.0f));
                if (!_isCooling)
                {
                    _isCooling = true;
                    StartCoroutine(poisionAttack(0.5f));  /// currentely being called too fast -> need too cooldown on each attack (enemy taking really fast hits)
                }

            }
            else if (_currentHealth._health == _prevHealth) // not being attacked
            {
                _isAttacked = false;
                if (!_isCooling)
                {
                    _isCooling = true;
                    StartCoroutine(poisionAttack(0.5f));  /// currentely being called too fast -> need too cooldown on each attack (enemy taking really fast hits)
                }
            }
            else if (_currentHealth._health > _prevHealth) //healing maybe
            {
                _isAttacked = false;
                _prevHealth = _currentHealth._health;
            }
        }

    }

    //checks if enemy has collidered with plant, plant is being attacked, and if there is currently no other enemy plant if focused on
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyTag") && _isAttacked && _enemy == null)
        {
            _enemy = other.gameObject;
        }
    }

    IEnumerator updateHealth(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _prevHealth = _currentHealth._health;
    }

    IEnumerator poisionAttack(float waitTime) // damages the targeted enemy        could cool down when attack made
    {
        if (_enemy != null)
        {
            _enemy.GetComponent<Health>().Damage(attackDamage);
        }
        yield return new WaitForSeconds(waitTime);
        _isCooling = false;
    }

}
