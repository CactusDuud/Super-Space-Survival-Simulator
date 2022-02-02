// Written by Sage Mahmud

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class LightAtNight : MonoBehaviour
{
    Light2D _light;
    float _defaultIntensity;

    void Awake()
    {
        _light = GetComponent<Light2D>();
        _defaultIntensity = _light.intensity;
    }

    void Start()
    {
        GameManager.GetInstance.OnDaytime += TurnOff;
        GameManager.GetInstance.OnNighttime += TurnOn;
    }

    void TurnOn()
    {
        _light.intensity = _defaultIntensity;
    }

    void TurnOff()
    {
        _light.intensity = 0;
    }
}
