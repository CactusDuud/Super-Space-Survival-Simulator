using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropDeath : Growth
{

    

    public override void Harvest()
    {
        if (_isHarvestable)
        {
           if (_health.IsAlive())
           {
                GameManager.GetInstance.AddProsperity(_prosperityValue);
                Destroy(this.gameObject);
            }
           else Destroy(this.gameObject);

        }



    }



}
