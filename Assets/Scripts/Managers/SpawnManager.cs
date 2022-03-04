// Written by Sage Mahmud

using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Singleton Insurance")]
    private static SpawnManager _instance;
    public static SpawnManager GetInstance { get { return _instance; } }

    [Header("References")]
    [SerializeField] GameObject[] _enemies;
    [SerializeField] GameObject _spawnPointParent;
    [SerializeField] Transform enemyParent;
    List<GameObject> _livingEnemies = new List<GameObject>();

    [Header("Enemy Variables")]
    [SerializeField] float _spawnInterval = 20.0f;
    bool _doEnemySpawning;
    float _spawnTimer;
    int _spawnCap = 12;

    
    void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this; 

        foreach (GameObject _enemy in GameObject.FindGameObjectsWithTag("EnemyTag"))
        {
            AddEnemy(_enemy);
        }
    }

    void Start()
    {
        GameManager.GetInstance.OnDaytime += DisbleSpawning;
        GameManager.GetInstance.OnNighttime += EnableSpawning;
    }

    void FixedUpdate()
    {
        if (_doEnemySpawning)
        {
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer >= _spawnInterval) SpawnEnemies();
        }
    }

    void SpawnEnemies()
    {
        _spawnTimer = 0.0f;
        GameObject _enemy = _enemies[Random.Range(0, _enemies.Length - 1)];
        Transform _spawnPoint = _spawnPointParent.transform.GetChild(Random.Range(0, _spawnPointParent.transform.childCount - 1));
        GameObject _newEnemy = Instantiate(_enemy, _spawnPoint.position, Quaternion.identity, enemyParent);
        _newEnemy.transform.position = new Vector3(_newEnemy.transform.position.x, _newEnemy.transform.position.y, 0);
        _livingEnemies.Add(_newEnemy);
    }

    void EnableSpawning() {_doEnemySpawning = true;}
    void DisbleSpawning() {_doEnemySpawning = false;}

    public bool IsLivingEnemies()
    {
        return _livingEnemies.Count > 0;
    }

    public GameObject GetRandomEnemy()
    {
        if (_livingEnemies.Count > 0) return _livingEnemies[Random.Range(0, _livingEnemies.Count - 1)];
        else return null;
    }

    public void AddEnemy(GameObject enemy)
    {
        _livingEnemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        _livingEnemies.Remove(enemy);
    }
}
