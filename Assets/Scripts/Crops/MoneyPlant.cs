// written by Elizabeth Castreje
// this script is an add on to a crop prefab to add variation to the crop family. if the plant is alive, it will add prosperity 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MoneyPlant : Growth
{
    // this is the player will gain "gainedProsperity" prosperity after "timeTpProsper" amount of seconds

    [SerializeField]
    float timeToProsper = 1f;

    [SerializeField]
    int gainedProsperity = 1;

  
    
    float _elapsed = 0f;
    
    void Update()
    {
        _elapsed += Time.deltaTime;
        if (_elapsed >= timeToProsper && _isWithered == false)
        {
            _elapsed = _elapsed % 1f;
            GameManager.GetInstance.AddProsperity(gainedProsperity);
        }
        
        if (_isWithered == true)
        {
            gainedProsperity = 0;
        }
    }
}