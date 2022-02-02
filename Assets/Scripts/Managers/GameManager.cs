// Written by Sage Mahmud

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Singleton Insurance")]
    private static GameManager _instance;
    public static GameManager GetInstance { get { return _instance; } }
    [SerializeField] GameObject[] _enemies;

    [Header("State Variables")]
    [SerializeField] private GameState state;

    [Header("Time Variables")]
    [Tooltip("Length of a day in seconds")]
    [SerializeField] int _dayLength = 1800;
    [SerializeField] float _clockTime = 0;

    [Header("Light Variables")]
    [SerializeField] Light2D _globalLight;
    [SerializeField] Color _dayColor;
    [SerializeField] float _daylightIntensity = 1.5f;
    [SerializeField] Color _nightColor;
    [SerializeField] float _nightlightIntensity = 0.5f;

    [Header("Enemy Variables")]
    [SerializeField] float _spawnInterval = 20.0f;
    bool _doEnemySpawning;
    float _spawnTimer;
    float[] _cameraRanges = new float[2] {-1.1f, 1.1f};

    [Header("Score Variables")]
    public int prosperity;

    [Header("Game UI")]
    public GameObject GameOverMenu;
    public TextMeshProUGUI stateText;

    public enum GameState
    {
        Day,
        Night,
        Lose
    }

    public void SetGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.Day:
                stateText.text = "Day";
                Daytime();
                break;
            case GameState.Night:
                stateText.text = "Night";
                Nighttime();
                break;
            case GameState.Lose:
                stateText.text = "This IS THE END";
                GameOverMenu.SetActive(true);
                Time.timeScale = 0f;
                break;
            default:
                break;
        }

        OnGameStateChanged?.Invoke();
    }

    #region Events
    public event System.Action OnGameStateChanged;

    public event System.Action OnDaytime;
    public void Daytime()
    {
        OnDaytime?.Invoke();
        _globalLight.intensity = _daylightIntensity;
        _globalLight.color = _dayColor;
        _doEnemySpawning = false;
    }

    public event System.Action OnNighttime;
    public void Nighttime()
    {
        OnNighttime?.Invoke();
        _globalLight.intensity = _nightlightIntensity;
        _globalLight.color = _nightColor;
        _doEnemySpawning = true;
    }

    public event System.Action OnAddProsperity;
    public void AddProsperity(int amount)
    {
        OnAddProsperity?.Invoke();
        prosperity += amount;
    }
    #endregion

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        
        SetGameState(GameState.Day);
    }

    void Start()
    {
    }

    void FixedUpdate()
    {
        // Day/night clock
        _clockTime += Time.deltaTime;
        if (_doEnemySpawning && _clockTime >= _dayLength) _clockTime = 0.0f;

        // Spawn clock
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= _spawnInterval) SpawnEnemies();

        if (_clockTime <= _dayLength * 0.5) SetGameState(GameState.Day);
        else if (_clockTime <= _dayLength) SetGameState(GameState.Night);
    }

    void SpawnEnemies()
    {
        _spawnTimer = 0.0f;
        Vector3 _cameraVector = new Vector3(
            _cameraRanges[Random.Range(0, _cameraRanges.Length - 1)],
            0.5f,
            10.0f
            );
        Vector3 _spawnPoint = Camera.main.ViewportToWorldPoint(_cameraVector);
        Instantiate(_enemies[Random.Range(0, _enemies.Length - 1)], _spawnPoint, Quaternion.identity);
    }

    public float GetTimePercent()
    {
        return _clockTime / _dayLength;
    }

    public float GetTimeRaw()
    {
        return _clockTime;
    }
}