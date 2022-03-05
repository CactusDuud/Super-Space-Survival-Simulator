// written by Elizabeth Castreje
// this script is an add on to a crop prefab to add variation to the crop family. if the plant is alive, it will add prosperity 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoneyPlant : MonoBehaviour
{
    // this is the player will gain 1 prosperity after "timeTpProsper" amount of seconds
    [SerializeField]
    float timeToProsper = 0f;
  
    
    float _elapsed = 0f;
    
    void Update()
    {
        _elapsed += Time.deltaTime;
        if (_elapsed >= timeToProsper )//&& Health._health != 0)
        {
            _elapsed = _elapsed % 1f;
            GameManager.GetInstance.AddProsperity(1);

        }
    }
}