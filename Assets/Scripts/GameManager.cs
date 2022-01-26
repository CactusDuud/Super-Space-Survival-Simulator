using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GameManager : MonoBehaviour
{
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

    void FixedUpdate()
    {
        _clockTime += Time.deltaTime;
        if (_clockTime >= _dayLength) {_clockTime = 0;}
        ManageGlobalLight();
    }

    void ManageGlobalLight()
    {
        if (_clockTime <= _dayLength * 0.5)
        {
            _globalLight.intensity = _daylightIntensity;
            _globalLight.color = _dayColor;
        }
        else if (_clockTime <= _dayLength)
        {
            _globalLight.intensity = _nightlightIntensity;
            _globalLight.color = _nightColor;
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