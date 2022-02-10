// Written by Sage Mahmud

using UnityEngine;
using TMPro;

[RequireComponent(typeof(TimeManager))]
public class GameManager : MonoBehaviour
{
    [Header("Singleton Insurance")]
    private static GameManager _instance;
    public static GameManager GetInstance { get { return _instance; } }

    [Header("State Variables")]
    [SerializeField] private GameState state;
    private bool _gameOver;

    [Header("References")]
    private TimeManager timeManager;
    private SpawnManager spawnManager;

    [Header("Score Variables")]
    public int prosperity;
    public int livingCrops;

    [Header("Game UI")]
    [SerializeField] TextMeshProUGUI prosperityText;
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
                stateText.text = "GAME OVER";
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
    public void Daytime() { OnDaytime?.Invoke(); }

    public event System.Action OnNighttime;
    public void Nighttime() { OnNighttime?.Invoke(); }
    #endregion

    void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this; 

        timeManager = GetComponent<TimeManager>();
        spawnManager = GetComponent<SpawnManager>();
        
        SetGameState(GameState.Day);

        prosperityText.text = $"Prosperity: {prosperity}";
    }

    void Update()
    {
        if (livingCrops <= 0)
        {
            _gameOver = true;
            SetGameState(GameState.Lose);
        }

        if (!_gameOver)
        {
            if (timeManager.IsDaytime()) SetGameState(GameState.Day);
            else if (timeManager.IsNighttime()) SetGameState(GameState.Night);
        }
    }

    public void AddProsperity(int amount)
    {
        prosperity += amount;
        prosperityText.text = $"Prosperity: {prosperity}";
    }

    public void SubtractProsperity(int amount)
    {
        prosperity -= amount;
        prosperityText.text = $"Prosperity: {prosperity}";
    }
}