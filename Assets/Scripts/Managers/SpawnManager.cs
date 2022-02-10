using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject[] _enemies;
    [SerializeField] GameObject _spawnPointParent;
    [SerializeField] Transform enemyParent;

    [Header("Enemy Variables")]
    [SerializeField] float _spawnInterval = 20.0f;
    bool _doEnemySpawning;
    float _spawnTimer;
    float[] _cameraRanges = new float[2] {-1.1f, 1.1f};
    [SerializeField] int _enemySpawnCap = 8;
    public int enemyCount;

    // Start is called before the first frame update
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
        enemyCount++;
        GameObject _enemy = _enemies[Random.Range(0, _enemies.Length - 1)];
        Transform _spawnPoint = _spawnPointParent.transform.GetChild(Random.Range(0, _spawnPointParent.transform.childCount - 1));
        GameObject _newEnemy = Instantiate(_enemy, _spawnPoint.position, Quaternion.identity, enemyParent);
        _newEnemy.transform.position = new Vector3(_newEnemy.transform.position.x, _newEnemy.transform.position.y, 0);
    }

    void EnableSpawning() {_doEnemySpawning = true;}
    void DisbleSpawning() {_doEnemySpawning = false;}
}
