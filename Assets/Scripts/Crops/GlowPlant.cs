// Written by Sebastian Carbajal and Sage Mahmud

using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


[RequireComponent(typeof(Light2D))]
public class GlowPlant : Growth
{
    Light2D light1;
    float defLight;
    [SerializeField] float lightDuration;



    protected override void Awake()
    {
        base.Awake();

        light1 = GetComponent<Light2D>();
        defLight = light1.intensity;
        light1.intensity = 0;
    }

    public override void Harvest()
    {
        // If the crop is ready to harvest...
        if (_isHarvestable)
        {
            // If alive, count it for points and reset it, otherwise just destroy it
            if (_health.IsAlive())
            {
                _spriteRenderer.sprite = _growingSprite;
                StartCoroutine(tempLight(lightDuration));
                _isHarvestable = false;
                GameManager.GetInstance.AddProsperity(_prosperityValue);
                _growthTimer = 0;
            }
            else Destroy(this.gameObject);
        }
    }

    IEnumerator tempLight(float dura1)
    {
        light1.intensity = defLight;
        yield return new WaitForSeconds(dura1);
        light1.intensity = 0;
    }
}
