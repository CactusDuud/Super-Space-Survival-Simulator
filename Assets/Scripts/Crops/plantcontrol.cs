using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantcontrol : MonoBehaviour
{
    // Start is called before the first frame update
    //attach this pretty much to most, if not all plants

    public Sprite noPlantObj;

    //this will be the sprout
    public Sprite sunFlower1;
    //this is full grown
    public Sprite sunFlower2;

    public Sprite potato1;
    public Sprite potato2;

    public Sprite carrot1;
    public Sprite carrot2;

    public float growTime = 0;

    public Transform plotObj;
    public string watered = "n";

    public string currentSeed="";

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSeed != "")
        {
            growTime += Time.deltaTime;
        }

        //in other words, if not watered and the time since planted has exceeded 5 seconds, then plant dies
        if ((growTime > 5) && (watered == "n"))
        {
            currentSeed = "";
            growTime = 0;
            GetComponent<SpriteRenderer>().sprite = noPlantObj;
        }

        if ((growTime > 5) && (watered == "y"))
        {
            if (currentSeed == "sunflower")
            {
                GetComponent<SpriteRenderer>().sprite = sunFlower2;
            }

            if (currentSeed == "carrot")
            {
                GetComponent<SpriteRenderer>().sprite = carrot2;
            }

            if (currentSeed == "potato")
            {
                GetComponent<SpriteRenderer>().sprite = potato2;
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("clicked on weed");
        if (GMScript.currentTool == "scythe")
        {
            GetComponent<SpriteRenderer>().sprite = noPlantObj;

        }

        //checks to make sure not only that you switched tools, but that there are no weeds
        //in other words, can't just straight up plant seeds without reaping
        if ((GMScript.currentTool == "sunflower") && (GetComponent<SpriteRenderer>().sprite == noPlantObj))
        {
            GetComponent<SpriteRenderer>().sprite = sunFlower1;
            currentSeed = "sunflower";
        }

        if ((GMScript.currentTool == "carrot") && (GetComponent<SpriteRenderer>().sprite == noPlantObj))
        {
            GetComponent<SpriteRenderer>().sprite = carrot1;
            currentSeed = "carrot";
        }

        if ((GMScript.currentTool == "potato") && (GetComponent<SpriteRenderer>().sprite == noPlantObj))
        {
            GetComponent<SpriteRenderer>().sprite = potato1;
            currentSeed = "potato";

        }


        if ((GMScript.currentTool == "bucket") && (GetComponent<SpriteRenderer>().sprite == noPlantObj))
        {
            //turns tile blue, indicating watered, and confirms it with variable
            plotObj.GetComponent<SpriteRenderer>().color = new Color (0,0,1);
            watered = "y";

        }
    }
}
