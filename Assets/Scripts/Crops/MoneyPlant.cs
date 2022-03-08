// written by Elizabeth Castreje and Sage Mahmud
// this script is an add on to a crop prefab to add variation to the crop family. if the plant is alive, it will add prosperity 

using UnityEngine;

[RequireComponent(typeof(Growth))]
[RequireComponent(typeof(Health))]
public class MoneyPlant : MonoBehaviour
{
    // this is the player will gain "gainedProsperity" prosperity after "timeTpProsper" amount of seconds

    Growth _growth;

    [SerializeField]
    float timeToProsper = 1f;

    [SerializeField]
    int gainedProsperity = 1;

  
    
    float _elapsed = 0f;

    void Awake()
    {
        _growth= GetComponent<Growth>();
    }
    
    void Update()
    {
        _elapsed += Time.deltaTime;
        if(_growth.IsHarvestable() && _elapsed >= timeToProsper)
        {
            _elapsed = _elapsed % timeToProsper;
            GameManager.GetInstance.AddProsperity(gainedProsperity);
        }
    }
}