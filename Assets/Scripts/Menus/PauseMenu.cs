using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //contributors: Esme, Miguel
    /*This script is used in the pause menu in order too allow the player too 
     * pause their game
     * resume
     * return too main menu*/

    //this function resumes the game from its paused state
    public void Resume()
    {
        Time.timeScale = 1f;
    }

    //this function pauses the game
    public void Pause() 
    {
        Time.timeScale = 0f;
    }

    //this function resumes the game and then loads the main menu scene
    public void ReturnMain() 
    {
        Resume();
        SceneManager.LoadScene(0);
    }
   
}
