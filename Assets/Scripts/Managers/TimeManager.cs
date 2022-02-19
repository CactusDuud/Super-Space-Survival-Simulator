// Written by Sage Mahmud

using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TimeManager : MonoBehaviour
{
    [Header("Singleton Insurance")]
    private static TimeManager _instance;
    public static TimeManager GetInstance { get { return _instance; } }

    [Header("Time Variables")]
    [Tooltip("Length of a day/night in seconds")]
    [SerializeField] public static int _dayLength = 180;
    [SerializeField] float _clockTime = 0;

    [Header("Light Variables")]
    [SerializeField] Light2D _globalLight;
    [SerializeField] Color _dayColor;
    [SerializeField] float _daylightIntensity = 1.5f;
    [SerializeField] Color _nightColor;
    [SerializeField] float _nightlightIntensity = 0.5f;

    
    void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this; 
    }

    void Start()
    {
        GameManager.GetInstance.OnDaytime += Daylight;
        GameManager.GetInstance.OnNighttime += Nightlight;
    }


    void FixedUpdate()
    {
        // Day/night clock
        _clockTime += Time.deltaTime;
        if (_clockTime >= _dayLength) _clockTime = 0.0f;
    }

    void Daylight()
    {
        _globalLight.intensity = _daylightIntensity;
        _globalLight.color = _dayColor;
    }

    void Nightlight()
    {
        _globalLight.intensity = _nightlightIntensity;
        _globalLight.color = _nightColor;
    }

    public float GetTimePercent()
    {
        return _clockTime / _dayLength;
    }

    public float GetTimeRaw()
    {
        return _clockTime;
    }

    public bool IsDaytime()
    {
        return _clockTime <= _dayLength * 0.5;
    }

    public bool IsNighttime()
    {
        return _clockTime > _dayLength * 0.5;
    }
}
