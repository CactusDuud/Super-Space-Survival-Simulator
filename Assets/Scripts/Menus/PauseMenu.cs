//contributors: Esmeralda Juarez, Miguel Aleman
/*This script is used in the pause menu in order too allow the player too 
 * pause their game
 * resume
 * return too main menu*/

 //side note: this script was also used in the Game Over Menu since it had the code needed (just didnt change the name of the script)

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
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
