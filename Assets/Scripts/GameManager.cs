using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    [Header("Singleton Insurance")]
    private static GameManager _instance;
    public static GameManager GetInstance { get { return _instance; } }

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

    void FixedUpdate()
    {
        _clockTime += Time.deltaTime;
        if (_clockTime >= _dayLength) {_clockTime = 0;}
        ManageGlobalLight();
    }

    #region Events
    public event System.Action onDaytime;
    public void Daytime()
    {
        onDaytime?.Invoke();
        _globalLight.intensity = _daylightIntensity;
        _globalLight.color = _dayColor;
    }

    public event System.Action onNighttime;
    public void Nighttime()
    {
        onNighttime?.Invoke();
        _globalLight.intensity = _nightlightIntensity;
        _globalLight.color = _nightColor;
    }

    public event System.Action onAddProsperity;
    public void AddProsperity(int amount)
    {
        onAddProsperity?.Invoke();
        prosperity += amount;
    }
    #endregion

    void ManageGlobalLight()
    {
        if (_clockTime <= _dayLength * 0.5)
        {
            Daytime();
        }
        else if (_clockTime <= _dayLength)
        {
            Nighttime();
        }
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