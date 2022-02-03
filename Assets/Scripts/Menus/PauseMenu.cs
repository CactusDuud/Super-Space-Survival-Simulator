//contributors: Esmeralda Juarez, Miguel Aleman
/*This script is used in the pause menu in order too allow the player too 
 * pause their game
 * resume
 * return too main menu*/

 //side note: this script was also used in the Game Over Menu since it had the code needed (just didnt change the name of the script)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //contributors: Esmeralda Juarez, Miguel Aleman
    /*This script is used in the pause menu in order too allow the player too 
     * pause their game
     * resume
     * return too main menu*/

    // checks to see if game is paused which it is not when starting so is set to false.
    public static bool GamePaused = false;

    // allows us to out the pausemenu in this object to show to players
    public GameObject PauseUI;

    //This update function checks to see if the escape button has been pushed, will then bring up pause menu
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    //this function resumes the game from its paused state
    public void Resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    //this function pauses the game
    public void Pause() 
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    //this function resumes the game and then loads the main menu scene
    public void ReturnMain() 
    {
        Resume();
        SceneManager.LoadScene(0);
    }
   
}
