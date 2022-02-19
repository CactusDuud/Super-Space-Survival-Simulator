//written by Miguel Aleman, Elizabeth Castreje

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTimer : MonoBehaviour
{
    //gets access to the UI slider on the canvas
    public Slider timeSlider;

    //sets a gametime based on what we input

    private float gameTime = TimeManager._dayLength / 2; 

    private float countTime;


    //is timer stopped?
    private bool stopTimer;
   
    void Start()
    {
       
        //makes counter equal to time
        countTime = gameTime;

        //sets the values of the time to show on timer
        stopTimer = false;
        timeSlider.maxValue = countTime;
        timeSlider.value = countTime;
    }

  
    void Update()
    {
        //decreases time by a sec
        countTime -= Time.deltaTime;


        if (countTime <= 0)
        {
            resetTimer();
           
        }
        if (stopTimer == false)
        {
            timeSlider.value = countTime;
        }
        
    }
    public void resetTimer()
    {
        //resets timer once it hits zero
        countTime = gameTime;
    }
}
