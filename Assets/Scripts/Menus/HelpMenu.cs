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

    public static bool HelpOpen = false;

    public static GameObject HelpUI;

    //void Start()
    //{
    //    //PauseMenu.TogglePause();
    //}

    void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;
    }



    public static void CloseHelpMenu()
    {
        HelpUI.SetActive(false);


    }

    public static void OpenHelpMenu()
    {
        HelpUI.SetActive(true);

    }
}
