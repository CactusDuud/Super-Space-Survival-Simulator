using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    [Header("Time Variables")]
    [Tooltip("Length of a day in seconds")]
    [SerializeField] int _day_length = 1800;
    [SerializeField] int _clock_time = 0;

    [Header("Light Variables")]
    [SerializeField] Light2D _global_light;
    [SerializeField] float _day_light_intensity = 1.5f;
    [SerializeField] float _night_light_intensity = 0.5f;
    [SerializeField] Color _day_color;
    [SerializeField] Color _night_color;

    [Header("Score Variables")]
    public int prosperity;

    void FixedUpdate()
    {
        _clock_time += 1;
        if (_clock_time >= _day_length) {_clock_time = 0;}
        ManageGlobalLight();
    }

    void ManageGlobalLight()
    {
        if (_clock_time <= _day_length * 0.5)
        {
            _global_light.intensity = _day_light_intensity;
            _global_light.color = _day_color;
        }
        else if (_clock_time <= _day_length)
        {
            _global_light.intensity = _night_light_intensity;
            _global_light.color = _night_color;
        }
    }

    public float GetTimePercent()
    {
        return _clock_time / _day_length;
    }

    public int GetTimeRaw()
    {
        return _clock_time;
    }
}