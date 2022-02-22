//written by Miguel Aleman, Elizabeth Castreje

// this script controls the button response to make the help information UI pop up

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpMenu : MonoBehaviour

{
    [Header("Singleton Insurance")]
    private static HelpMenu _instance;
    public static HelpMenu GetInstance { get { return _instance; } }

    //help keep track of when the help menu is open or closed
    public static bool HelpOpen = false;

    //this will help us be able to hide the game object from the player later in code with SetActive
    public GameObject HelpUI;


    void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;
    }


    public void ToggleHelp()
    {
        if (HelpOpen)
        {
            CloseHelpMenu();
        }
        else
        {
            OpenHelpMenu();
        }
    }


    // TEAM NOTES~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // we currently do not have this menu shown as it is not funcitoning as intended, problematic code is commented out and the button is inactive in unity 

    public static void CloseHelpMenu()
    {
        //HelpUI.SetActive(false);
        HelpOpen = false;


    }

    public static void OpenHelpMenu()
    {
        //HelpUI.SetActive(true);
        Debug.Log("this is working");
        HelpOpen = true;

    }
}
