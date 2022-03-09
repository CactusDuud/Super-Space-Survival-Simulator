// Written by Sage Mahmud

using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class LightAtNight : MonoBehaviour
{
    Light2D _light;
    float _defaultIntensity;

    void Awake()
    {
        // Save necessary variables
        _light = GetComponent<Light2D>();
        _defaultIntensity = _light.intensity;
    }

    void Start()
    {
        // Subscribe to day/night cycle for turning on and off the light
        GameManager.GetInstance.OnDaytime += TurnOff;
        GameManager.GetInstance.OnNighttime += TurnOn;
    }

    void TurnOn()
    {
        // Turn on the light by setting its intensity to the value set in the editor
        _light.intensity = _defaultIntensity;
    }

    void TurnOff()
    {
        // Turn off the light by setting its intensity to zero
        _light.intensity = 0;
    }
}
