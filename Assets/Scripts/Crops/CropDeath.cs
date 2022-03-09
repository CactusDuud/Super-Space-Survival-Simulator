// code written by Miguel Aleman

//job of this script is to kill a plant the moment it is harvested

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
           }
           
           Destroy(this.gameObject);
        }
    }
}
