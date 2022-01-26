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

    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetInstance.onDaytime += TurnOff;
        GameManager.GetInstance.onNighttime += TurnOn;
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
