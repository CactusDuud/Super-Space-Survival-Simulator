// Written by Sage Mahmud

using UnityEngine;
using TMPro;
using Photon.Pun;

[RequireComponent(typeof(TimeManager))]
public class GameManager : MonoBehaviour
{
    [Header("Singleton Insurance")]
    private static GameManager _instance;
    public static GameManager GetInstance { get { return _instance; } }

    [Header("State Variables")]
    [SerializeField] private GameState state;

    [Header("References")]
    private TimeManager _timeManager;
    private SpawnManager _spawnManager;
    private CropManager _cropManager;

    [Header("Score Variables")]
    public int prosperity;

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

        _timeManager = GetComponent<TimeManager>();
        _spawnManager = GetComponent<SpawnManager>();
        _cropManager = GetComponent<CropManager>();
        
        SetGameState(GameState.Day);

        prosperityText.text = $"Prosperity: {prosperity}";
    }

    void Update()
    {
        if (!_cropManager.IsLivingCrop()) SetGameState(GameState.Lose);
        else
        {
            if (_timeManager.IsDaytime()) SetGameState(GameState.Day);
            else if (_timeManager.IsNighttime()) SetGameState(GameState.Night);
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