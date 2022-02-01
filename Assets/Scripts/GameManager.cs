using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    [Header("Singleton Insurance")]
    private static GameManager _instance;
    public static GameManager GetInstance { get { return _instance; } }

    [Header("State Variables")]
    [SerializeField] private GameState state;

    [Header("Time Variables")]
    [Tooltip("Length of a day in seconds")]
    [SerializeField] int _dayLength = 1800;
    [SerializeField] float _clockTime = 0;

    [Header("Light Variables")]
    [SerializeField] Light2D _globalLight;
    [SerializeField] float _daylightIntensity = 1.5f;
    [SerializeField] float _nightlightIntensity = 0.5f;
    [SerializeField] Color _dayColor;
    [SerializeField] Color _nightColor;

    [Header("Score Variables")]
    public int prosperity;

    public enum GameState
    {
        Init,
        Day,
        Night,
        Lose
    }

    public void SetGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.Init:
                GridManager.GetInstance.CreateGrid();


                break;
            case GameState.Day:
                Daytime();
                break;
            case GameState.Night:
                Nighttime();
                break;
            case GameState.Lose:
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
    }

    public event System.Action OnNighttime;
    public void Nighttime()
    {
        OnNighttime?.Invoke();
        _globalLight.intensity = _nightlightIntensity;
        _globalLight.color = _nightColor;
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
    }

    void Start()
    {
        SetGameState(GameState.Init);
        SetGameState(GameState.Day);
    }

    void FixedUpdate()
    {
        _clockTime += Time.deltaTime;
        if (_clockTime >= _dayLength) _clockTime = 0;

        if (_clockTime <= _dayLength * 0.5) SetGameState(GameState.Day);
        else if (_clockTime <= _dayLength) SetGameState(GameState.Night);
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