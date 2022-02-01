using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tools : MonoBehaviour
{
    // Start is called before the first frame update
    //attach this script to tools

    //cursor is just like an indicator showing which thing you have currently selected
    public Transform cursorObj;
    //this can probably be removed
    //make sure you have text set up with the textinfo script for this
    public Transform seedInvObj;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameObject.name == "scythe")
        {
            GMScript.currentTool = "scythe";
        }

        if (gameObject.name == "seeds")
        {
            GMScript.currentTool = "seeds";
        }

        if (gameObject.name == "bucket")
        {
            GMScript.currentTool = "bucket";
        }

        cursorObj.transform.position = transform.position;
        Debug.Log(GMScript.currentTool);

    }
}
