// code written by Miguel Aleman

//job of this script is to kill a plant the moment it is harvested

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropDeath : Growth
{

    

    public override void Harvest()
    {
        //looking to see if plant is harvestable
        if (_isHarvestable)
        {
            //changing it to remove plant instead of replanting
           if (_health.IsAlive())
           {
                GameManager.GetInstance.AddProsperity(_prosperityValue);
                base.Wither();
                Destroy(this.gameObject);
            }
           else Destroy(this.gameObject);

        }



    }



}
