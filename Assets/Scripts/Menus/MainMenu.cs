using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    //contributors: Esmeralda Juarez, Miguel Aleman
    /*This script is used in the main menu in order too allow the player too 
     * start game
     * quit game*/

    //this function loads the starting level scene so that the player can start playing
    //it also sends a message in the log too make sure it works
    public void StartButton()
    {
        Debug.Log("start");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //this function quits the game
    //it also sends a message in the log too make sure it works
    public void QuitButton()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    public void SetGameMode(int mode)
    {
        /*
        0 - Singleplayer
        1 - Multiplayer
        2 - Multiplayer Online
        */
        PlayerPrefs.SetInt("gameType", mode);
    }
}

